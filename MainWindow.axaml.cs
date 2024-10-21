using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks; // Ensure this is included
using Avalonia.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace bankscrape
{    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ScrapeAndDisplayTransactions();
        }

        private async void ScrapeAndDisplayTransactions()
        {
            var transactions = await ScrapeTransactions();
            TransactionDataGrid.ItemsSource = transactions;
        }

        public async Task<List<Transaction>> ScrapeTransactions()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless=old"); // Run in headless mode
            chromeOptions.AddArgument("--disable-gpu"); // Disable GPU acceleration
            chromeOptions.AddArgument("--no-sandbox"); // Avoid sandboxing issues
            using (IWebDriver driver = new ChromeDriver(chromeOptions))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                driver.Navigate().GoToUrl("https://digital.pnbank.com.au/");
                await Task.Delay(3000); // Wait for the page to load

                // Retrieve the arguments from the App class
                List<string> args = App.GetArguments();

                if (args.Count < 2)
                {
                    Console.WriteLine("Provide username and password as arguments.");
                    return new List<Transaction>(); // Return an empty list of transactions
                }

                // Use the username from the arguments
                IWebElement usernameField = driver.FindElement(By.Id("MemberNumber"));
                usernameField.SendKeys(args[0]); // Use the first argument as the username

                await Task.Delay(3000); // Wait before entering password

                // Use the password from the arguments
                IWebElement passwordField = driver.FindElement(By.Id("Password"));
                passwordField.SendKeys(args[1]); // Use the second argument as the password

                await Task.Delay(3000); // Wait before clicking login or next actions

                driver.FindElement(By.Id("loginButton")).Click();

                await Task.Delay(7000); // Wait for login
                List<Transaction> transactionData = new List<Transaction>();

                var ellipses = wait.Until(d => d.FindElements(By.CssSelector(".small-2.medium-2.large-1.columns.account-ellipsis.right.equalheight-watch.remove-outline")));
                if (ellipses.Count >= 3)
                {
                    ellipses[2].Click();
                }
                else
                {
                    Console.WriteLine("Not enough ellipsis elements found.");
                    return transactionData; // Return empty list if not found
                }


                await Task.Delay(3000); // Wait for transactions to load

                IList<IWebElement> transactionRows = driver.FindElements(By.CssSelector(".top-transaction.row.collapse"));
                

                foreach (IWebElement row in transactionRows)
                {
                    string transactionDate = row.FindElement(By.CssSelector(".columns.small-3.left-date")).Text;
                    string transactionDescription = row.FindElement(By.CssSelector(".columns.small-6")).Text;
                    string transactionAmount = row.FindElement(By.CssSelector(".columns.small-3.right.right-amt")).Text;

                    transactionData.Add(new Transaction { Date = transactionDate, Description = transactionDescription, Amount = transactionAmount });
                }
                
                
                Console.WriteLine("Transactions Captured.");
                IList<IWebElement> accountItems = driver.FindElements(By.CssSelector(".account-item.account-type-sav"));
                IWebElement fourthAccountSav = accountItems[3];
                IWebElement AccountSummary = fourthAccountSav.FindElement(By.CssSelector(".account-summary.row.collapse"));
                IWebElement AccountBalInfo = AccountSummary.FindElement(By.CssSelector(".account-bal-info.small-4.medium-4.large-4.columns"));
                IWebElement AccountBal = AccountBalInfo.FindElement(By.ClassName("account-bal"));
                var balances = AccountBal.FindElement(By.CssSelector(".dollar"));
                
                IWebElement parentContainer = balances.FindElement(By.XPath(".."));
                Balance.Text = parentContainer.Text;

                Console.WriteLine("Balance Captured.");
                return transactionData;
            }
        }
    }

    public class Transaction
    {
        public string? Date { get; set; } // Nullable
        public string? Description { get; set; } // Nullable
        public string? Amount { get; set; } // Nullable
    }
}
