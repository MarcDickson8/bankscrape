# Bank Transaction Scraper

This program scrapes bank transaction information from [PN Bank](https://digital.pnbank.com.au/) and displays the data in an Avalonia GUI. It uses Selenium for automated web interaction and retrieving transaction details securely.

## Features

- **User Authentication**: Log in using your bank credentials (username and password).
- **Transaction Retrieval**: Automatically fetches and displays transaction history.
- **Balance Display**: Shows the account balance for your selected account.
- **Cross-Platform GUI**: Built with Avalonia, making it compatible with multiple operating systems.

## Prerequisites

- Your bank is P&N bank.
- [.NET SDK](https://dotnet.microsoft.com/download) (version 5.0 or higher)
- [Avalonia](https://avaloniaui.net/)
- [Google Chrome](https://www.google.com/chrome/) (for Selenium WebDriver)
- [Selenium WebDriver](https://www.selenium.dev/downloads/) (automatically managed by the program)


## Usage

    dotnet run <your-username> <your-password>

Replace <your-username> and <your-password> with your actual bank credentials.

The application will log in to your bank account, scrape your transaction data, and display it in the GUI.

Code Overview
The core functionality is contained within the MainWindow.axaml.cs file. Key components include:

ScrapeAndDisplayTransactions(): Initiates the scraping process and updates the UI with transaction data.

ScrapeTransactions(): Handles the logic for logging in, scraping transaction details, and handling the Selenium WebDriver.

Transaction Class:
The Transaction class is used to structure the transaction data with the following properties:

Date: The date of the transaction.
Description: A brief description of the transaction.
Amount: The amount involved in the transaction.

Contributing
Contributions are welcome! If you have suggestions or improvements, please open an issue or submit a pull request.

Disclaimer
This program interacts with your bank account. Ensure you understand the security implications of using automated login methods and manage your credentials responsibly. Use this program at your own risk.

This README provides clear instructions on how to use the program, and it informs users of the prerequisites and the functionality offered.
