using Chatty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberChat
{
    internal class ChatBot
    {

        private bool NameCaptured = false;
        private TextBox MsgInput;
        private TextBlock BotQuestionText;
        private ListBox ChatBotArea;

        public ChatBot(TextBox MsgInput)
        {
            MsgInput = MsgInput;
        }


        private void Send_Click(object sender, RoutedEventArgs e)
        {



            string userMessage = MsgInput.Text.Trim();


            if (string.IsNullOrEmpty(userMessage))
            {
                MemoryStore.UserName = userMessage;
                MessageBox.Show("Please enter your message");
                return;
            }



            if (!NameCaptured)
            {
                MemoryStore.UserName = userMessage;
                NameCaptured = true;

                BotQuestionText.Text = $"Hello, {MemoryStore.UserName} How can i help you Today, Type yes to process or any key to abort";

                ChatBotArea.Items.Add("CyberChatBot: Nice to meet you " + MemoryStore.UserName);
                MsgInput.Clear();
                return;

            }

            // Show user message
            ChatBotArea.Items.Add(MemoryStore.UserName + ": " + userMessage);

            // Bot reply
            string botReply = BotReplies(userMessage);

            ChatBotArea.Items.Add("CyberChatBot: " + botReply);

            // Clear input
            MsgInput.Clear();

        }


        private string BotReplies(string message)
        {
            Sentiments BotMood = new Sentiments();


            message = message.ToLower().Trim();



            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please enter your message");
                return "";
            }


            if (message.Contains("hello"))
            {

                return "hi" + MemoryStore.UserName;
            }
            else if (message.Contains("morning"))
            {
                return "Good Morning" + MemoryStore.UserName;
            }
            else
            {
                return "i didnt get get that please rephrase" + MemoryStore.UserName;
            }
        }



        //handle the enter key in case user enter to send msg 
        private readonly string PHolder = "Type Your Message...";

        private void MessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                // Stop if textbox is empty OR showing placeholder
                if (string.IsNullOrWhiteSpace(MsgInput.Text) ||
                    MsgInput.Text == PHolder)
                {
                    return;
                }

                Send_Click(this, new RoutedEventArgs());
                e.Handled = true;
                MsgInput.Focus();
            }
        }

        private void AnimateCursorGotFocus(object sender, RoutedEventArgs e)
        {
            if (MsgInput.Text == PHolder)
            {
                MsgInput.Text = "";
                MsgInput.Foreground = Brushes.Black;
            }
        }
        private void AnimateCursorLostFocus(object sender, RoutedEventArgs e)
        {     //restore the place if the user leave the box
            if (string.IsNullOrEmpty(MsgInput.Text))
            {
                MsgInput.Text = PHolder;
                MsgInput.Foreground = Brushes.Gray;
            }
        }

        private void TextBoxBotArea_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void advanceTopics(object sender, RoutedEventArgs e)
        {


            MessageBoxResult results = MessageBox.Show("Do you want to learn more about security?", "Next Topic", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (results == MessageBoxResult.Yes)
            {
                KeywordResponder kr = new KeywordResponder();
                string response = kr.GetResponse("password");
                kr.GetResponse("Password" + response);
            }

        }
    }
}
