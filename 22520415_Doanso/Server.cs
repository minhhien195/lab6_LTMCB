using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _22520415_Doanso
{
    public partial class Server : Form
    {
        private List<ClientConnection> clientConnections = new List<ClientConnection>();
        private Random random = new Random();
        private const int MinimumPlayers = 2; // Số lượng người chơi tối thiểu
        private int secretNumber;
        private int a;
        private int b;
        private int currentRound = 0;
        private int totalRounds;
        private int countdown;
        private int guessesReceived = 0; // Số lượng kết quả đã nhận được
        private Dictionary<string, PlayerInfo> playerScores;
        private List<string> readyPlayers = new List<string>(); // Danh sách người chơi đã sẵn sàng
        public Server()
        {
            InitializeComponent();
        }
        private void Server_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread serverThread = new Thread(new ThreadStart(StartSafeThread));
            serverThread.Start();
            playerScores = new Dictionary<string, PlayerInfo>();
        }
        private void StartSafeThread()
        {
            Socket listenerSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            listenerSocket.Bind(serverEndPoint);
            listenerSocket.Listen(-1);
            rtbMess.AppendText("Server running on 127.0.0.1:8080\n");

            while (true)
            {
                Socket clientSocket = listenerSocket.Accept();
                HandleClient(clientSocket);
            }
        }
        private void InitializeGame()
        {
            
            if (currentRound == 0)
            {
                readyPlayers.Clear();
                BroadcastMessage("Server: Ready to start the game? (Type 'ready' to proceed)\n", null);
            }
            
        }
        private void HandleClient(Socket clientSocket)
        {
            Thread clientThread = new Thread(() =>
            {
                string clientName = string.Empty;
                try
                {
                    string clientAddress = ((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString();
                    int clientPort = ((IPEndPoint)clientSocket.RemoteEndPoint).Port;
                    rtbMess.AppendText($"New player connected from: {clientAddress}: {clientPort}\n");
                    byte[] recvBuffer = new byte[1024];

                    while (true)
                    {
                        int bytesReceived = clientSocket.Receive(recvBuffer);
                        if (bytesReceived == 0)
                        {
                            break;
                        }

                        string message = Encoding.UTF8.GetString(recvBuffer, 0, bytesReceived);
                        message = message.Trim();

                        if (string.IsNullOrEmpty(clientName))
                        {
                            clientName = message;
                            clientConnections.Add(new ClientConnection(clientSocket, clientName));
                            BroadcastMessage($"{clientName} has joined the game.\n", clientSocket);

                            // Gửi yêu cầu sẵn sàng khi có ít nhất một người chơi
                            if (clientConnections.Count >= MinimumPlayers)
                            {
                                InitializeGame();
                            }

                            UpdateGameInfo();
                        }
                        else if (message.ToLower() == "ready")
                        {
                            if (!readyPlayers.Contains(clientName))
                            {
                                readyPlayers.Add(clientName);
                                if (currentRound == 0)
                                {
                                    rtbMess.AppendText($"{clientName} is ready.\n");
                                    BroadcastMessage($"{clientName} is ready.\n", null);
                                }   

                                if (readyPlayers.Count == clientConnections.Count && clientConnections.Count >= MinimumPlayers)
                                {
                                    StartNewGame();
                                }
                            }
                        }
                        else if (int.TryParse(message, out int guess))
                        {
                            ProcessGuess(clientName, guess);
                        }
                        else
                        {
                            string serverMessage = $"{clientAddress}: {clientPort}: {message}\n";
                            rtbMess.AppendText(serverMessage);
                            BroadcastMessage($"{clientName}: {message}\n", null);
                        }
                    }
                }
                catch (SocketException)
                {
                    // Handle socket exceptions
                }
                finally
                {
                    clientSocket.Close();
                    clientConnections.RemoveAll(c => c.Socket == clientSocket);
                    if (playerScores.ContainsKey(clientName))
                    {
                        playerScores.Remove(clientName);
                    }
                    BroadcastMessage($"Server: {clientName} has left the game\n", null);
                    UpdateGameInfo();
                }
            });

            clientThread.Start();
        }
        private void StartNewGame()
        {
            lock (playerScores)
            {
                int distance = random.Next(10, 51);
                a = random.Next(1000);
                b = a + distance;
                secretNumber = random.Next(a + 1, b);
                totalRounds = random.Next(5, 8);
                currentRound = 1;
                countdown = random.Next(5, 11);
                UpdateGameInfo();
                string newGameMessage = $"\nNew game started. Find a number between {a} and {b}. Current round: {currentRound} Total rounds: {totalRounds} Time: {countdown}\n";
                rtbMess.AppendText(newGameMessage);
                BroadcastMessage(newGameMessage, null);

                tmCount.Start();
            }
        }
        private void ProcessGuess(string playerName, int guess)
        {
            lock (playerScores)
            {
                if (!playerScores.ContainsKey(playerName))
                {
                    playerScores[playerName] = new PlayerInfo();
                }

                PlayerInfo player = playerScores[playerName];
                player.TotalGuesses++;

                if (currentRound == 0)
                {
                    currentRound++;
                    UpdateGameInfo();
                }

                string result;
                if (guess == secretNumber)
                {
                    result = "Correct";
                    player.CorrectGuesses++;
                    player.Score += 10;
                    string mess = $"{playerName} guessed {guess}. {result}. {playerName} has score: {player.Score}\n";
                    rtbMess.AppendText(mess);
                    BroadcastMessage(mess, null);
                    playerScores[playerName].GuessSubmitted = true;
                }
                else
                {
                    result = guess < secretNumber ? "Too low" : "Too high";
                    player.Score -= 5;
                    string message = $"{playerName} guessed {guess}. {result}. {playerName} has score: {player.Score}\n";
                    rtbMess.AppendText(message);
                    BroadcastMessage(message, null);
                    playerScores[playerName].GuessSubmitted = true;
                }

                playerScores[playerName].GuessSubmitted = true;
                guessesReceived++; // Tăng biến đếm số lượng kết quả đã nhận được


                if (guessesReceived == clientConnections.Count && currentRound < totalRounds)
                {
                    if (int.Parse(lbCount.Text) > 0)
                    {
                        string endRoundMessage = $"Round {currentRound} has ended.\n";
                        rtbMess.AppendText(endRoundMessage);
                        BroadcastMessage(endRoundMessage, null);
                    }
                    
                    Thread.Sleep(1000);
                    InitializeNextRound();
                }
            }
        }
        

        private void InitializeNextRound()
        {
            lock (playerScores)
            {
                guessesReceived = 0; // Đặt số lượng kết quả đã nhận được về 0
                foreach (var playerInfo in playerScores.Values)
                {
                    playerInfo.GuessSubmitted = false;
                }

                int distance = random.Next(10, 51);
                a = random.Next(1000);
                b = a + distance;
                secretNumber = random.Next(a + 1, b);
                currentRound++;
                countdown = random.Next(5, 11);
                UpdateGameInfo();

                string newRoundMessage = $"\nNew round started. Find a number between {a} and {b}. Current round: {currentRound} Total rounds: {totalRounds} Time: {countdown}\n";
                rtbMess.AppendText(newRoundMessage + "\n");
                BroadcastMessage(newRoundMessage, null);
                tmCount.Start();
            }
        }
        private void EndGame()
        {
            lock (playerScores)
            {
                var winners = playerScores.OrderByDescending(p => p.Value.CorrectGuesses)
                                 .ThenBy(p => p.Value.TotalGuesses)
                                 .Where(p => p.Value.CorrectGuesses == playerScores.Max(ps => ps.Value.CorrectGuesses))
                                 .Select(p => p.Key)
                                 .ToList();

                string endMessage = "";
                if (winners.Count == 1)
                {
                    string winnerName = winners[0];
                    int correctGuesses = playerScores[winnerName].CorrectGuesses;
                    int totalGuesses = playerScores[winnerName].TotalGuesses;
                    endMessage = $"Game Over! Winner: {winnerName} with {correctGuesses} correct guesses and {totalGuesses} total guesses.\n";
                }
                else if (winners.Count > 1)
                {
                    endMessage = "Game Over! It's a tie between the following players: ";
                    endMessage += string.Join(", ", winners);
                    endMessage += ". They all have " + playerScores[winners[0]].CorrectGuesses + " correct guesses and " + playerScores[winners[0]].TotalGuesses + " total guesses.\n";
                }
                rtbMess.AppendText(endMessage);
                BroadcastMessage(endMessage, null);

                playerScores.Clear();
            }
        }

        private void UpdateGameInfo()
        {
            lbCount.Text = countdown.ToString();
            lbNumber.Text = secretNumber.ToString();
            lbRange.Text = $"[ {a}, {b} ]";
            lbPlayers.Text = clientConnections.Count.ToString();
            lbRound.Text = $"{currentRound}/{totalRounds}";
        }

        private void BroadcastMessage(string message, Socket senderSocket)
        {
            byte[] sendBuffer = Encoding.UTF8.GetBytes(message);

            foreach (var clientConnection in clientConnections)
            {
                if (clientConnection.Socket.Connected)
                {
                    clientConnection.Socket.Send(sendBuffer);
                }
            }
        }

        private void tmCount_Tick(object sender, EventArgs e)
        {
            if (int.Parse(lbCount.Text) > 0)
            {
                lbCount.Text = (int.Parse(lbCount.Text) - 1).ToString();

            }
            else if (int.Parse(lbCount.Text) == 0 && currentRound > 0)
            {
                tmCount.Stop(); // Dừng bộ đếm ngược
                if (guessesReceived == clientConnections.Count && currentRound < totalRounds)
                {
                    string endRoundMessage = $"Round {currentRound} has ended.\n";
                    rtbMess.AppendText(endRoundMessage);
                    BroadcastMessage(endRoundMessage, null);
                }
                else if (guessesReceived == clientConnections.Count && currentRound >= totalRounds)
                {
                    EndGame();
                }
                tmCount.Start();
            }
        }
    }

    public class ClientConnection
    {
        public Socket Socket { get; private set; }
        public string playerName { get; private set; }

        public ClientConnection(Socket socket, string Name)
        {
            Socket = socket;
            playerName = Name;
        }
    }

    public class PlayerInfo
    {
        public int CorrectGuesses { get; set; }
        public int TotalGuesses { get; set; }
        public int Score { get; set; }
        public bool GuessSubmitted { get; set; } // Thuộc tính mới
        public PlayerInfo()
        {
            Score = 0;
            CorrectGuesses = 0;
            TotalGuesses = 0;
            GuessSubmitted = false;
        }
    }
}
