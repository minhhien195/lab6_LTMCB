using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _22520415_Doanso
{
    public partial class Client : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;
        private string playerName;
        private int a = 1;
        private int b = 100;
        private int currentRound = 0;
        private int totalRounds = 0;
        private int countdown = 0; // Add this line
        private Random random = new Random();
        private Dictionary<string, bool> clientGuessEnabled;
        private bool countdownFinished = false;
        public Client()
        {
            InitializeComponent();
        }
        private void tmCount_Tick(object sender, EventArgs e)
        {
            if (countdown > 0)
            {
                countdown--; // Decrement the countdown variable
                lbCount.Text = countdown.ToString();
            }
            else if (countdown == 0 && currentRound > 0 && clientGuessEnabled[txtName.Text])
            {
                tmCount.Stop();
                countdownFinished = true;
                WaitForAllClientsToFinishCountdown();
            }
        }
        private void WaitForAllClientsToFinishCountdown()
        {
            List<string> clientKeys = clientGuessEnabled.Keys.ToList();

            foreach (string playerName in clientKeys)
            {
                int autoGuess = random.Next(a, b + 1);
                SendGuess(autoGuess);
                clientGuessEnabled[playerName] = false;
            }
            StartNewGuess(countdown);
        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            clientGuessEnabled = new Dictionary<string, bool>();
            playerName = txtName.Text;
            if (string.IsNullOrWhiteSpace(playerName))
            {
                MessageBox.Show("Please enter your name.");
                return;
            }

            try
            {
                client = new TcpClient("127.0.0.1", 8080);
                stream = client.GetStream();

                byte[] nameBuffer = Encoding.UTF8.GetBytes(playerName);
                clientGuessEnabled.Add(playerName, true);

                stream.Write(nameBuffer, 0, nameBuffer.Length);

                receiveThread = new Thread(ReceiveMessages);
                receiveThread.Start();

                btnReady.Enabled = false;
                btnSend.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}");
            }
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtNumber.Text, out int guess) && clientGuessEnabled[txtName.Text] == true)
            {
                SendGuess(guess);
                tmCount.Stop();
                clientGuessEnabled[txtName.Text] = false;
                txtNumber.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                SendMessage(txtMessage.Text);
                txtMessage.Clear();
            }
        }
        private void SendMessage(string message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}");
            }
        }
        private void ReceiveMessages()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    UpdateChatBox(message);

                    if (message.Contains("Server: Ready to start the game?"))
                    {
                        // Gửi "ready" khi nhận được yêu cầu sẵn sàng
                        SendMessage("ready");
                        lbWinner.Text = "";
                    }
                    else if (message.Contains("\nNew game started.") || message.Contains("\nNew round started."))
                    {
                        //MessageBox.Show($"{txtName.Text} - {message}");
                        string pattern = @"\nNew (game|round) started\. Find a number between (\d+) and (\d+)\. Current round: (\d+) Total rounds: (\d+) Time: (\d+)\n";
                        Match match = Regex.Match(message, pattern);

                        if (match.Success)
                        {
                            string gameType = match.Groups[1].Value;
                            a = int.Parse(match.Groups[2].Value);
                            b = int.Parse(match.Groups[3].Value);
                            currentRound = int.Parse(match.Groups[4].Value);
                            totalRounds = int.Parse(match.Groups[5].Value);
                            countdown = int.Parse(match.Groups[6].Value);

                            UpdateGameInfo();
                            ResetClientGuessEnabled();
                            StartNewGuess(countdown);

                        }
                    }

                    else if (message.Contains("Correct") || message.Contains("Game Over"))
                    {
                        if (message.Contains("Correct"))
                        {
                            // Tách tên người chơi đã đoán đúng từ thông báo
                            string[] correctPlayerName = message.Split(' ');

                            if (correctPlayerName[0] == playerName)
                            {
                                // Nếu client hiện tại là người đoán đúng
                                tmCount.Stop();
                                clientGuessEnabled[playerName] = false;
                            }
                        }

                        if (message.Contains("Game Over! Winner"))
                        {
                            tmCount.Stop();
                            SaveHistory();

                            // Extract winner
                            string keyword = "Winner:";
                            int winnerIndex = message.IndexOf(keyword);

                            if (winnerIndex != -1)
                            {
                                int startIndex = winnerIndex + keyword.Length;
                                int endIndex = message.IndexOf(" with", startIndex);
                                string winnerKey = message.Substring(startIndex, endIndex - startIndex).Trim();

                                lbWinner.Text = winnerKey;
                            }
                        }
                        else if (message.Contains("Game Over! It's a tie between the following players"))
                        {
                            lbWinner.Text = "Draw";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error receiving message: {ex.Message}");
            }
        }
        private void ResetClientGuessEnabled()
        {
            foreach (var key in clientGuessEnabled.Keys.ToList())
            {
                clientGuessEnabled[key] = true;
            }
        }
        private void UpdateGameInfo()
        {
            if (lbRange.InvokeRequired)
            {
                lbRange.Invoke(new Action(UpdateGameInfo));
            }
            else
            {
                lbRange.Text = $"[ {a}, {b} ]";
                lbRound.Text = $"{currentRound}/{totalRounds}";
            }
        }
        private void SendGuess(int guess)
        {
            SendMessage(guess.ToString());
        }
        private void StartNewGuess(int countdown)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(StartNewGuess), countdown);
                return;
            }
            this.countdown = countdown; // Update the countdown variable
            lbCount.Text = countdown.ToString();
            tmCount.Stop(); // Đảm bảo timer cũ đã dừng
            tmCount.Start(); // Bắt đầu timer mới
        }

        private void UpdateChatBox(string message)
        {
            if (rtbMessage.InvokeRequired)
            {
                rtbMessage.Invoke(new Action<string>(UpdateChatBox), message);
            }
            else
            {
                rtbMessage.AppendText(message);
            }
        }

        private void SaveHistory()
        {
            try
            {
                System.IO.File.WriteAllText("history.txt", rtbMessage.Text);
                MessageBox.Show("Game history saved to history.txt");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving history: {ex.Message}");
            }
        }

        private void Client_Load(object sender, EventArgs e)
        {
           
        }
    }
}
