# Backlog

## Accounts

As a potential customer I want to create a new bank account so I can manage my money.
- Accounts should be linked to a social networking login (Google, Facebook, Twitter)
- Unlike normal banking, we don’t need 100 points of validated identification. Ask for password number and/or drivers license, but don’t worry about validating them.
- Account numbers should follow the BSB format.
- All accounts need a name, with a minimum of 5 characters.
- All bank accounts require an initial deposit of at least $10

As an existing customer I want to be able to create new accounts
- New accounts can have initial funds sourced from deposits or transferred from an account held by the customer
- Account numbers should follow the BSB format
- Accounts need a name. Each account name should be a minimum of 5 characters and unique for accounts held by the customer.

As a customer I want to close an account that I am no longer using and withdraw any remaining funds as cash, or transfer them to another account
- An account closure reason is required.
- Closed accounts cannot be deleted.
- Accounts can only be closed by the account holder.

As a bank manager I want to know the personal details of my customers so I can comply with any legal requirements
- Mandatory details include full name, address, birth date
- Optional details include phone numbers, alternate email addresses, skype handle
- At least one optional detail must be provided

As a banking organization I only want to support transactions in Australian dollars so that my software systems are less complex.
- That means no foreign currencies and no exchange rate management
- Note: if you’ve built this whole thing and want to extend your work, feel free to add the functionality. You’ll need to consider as-at queries, buy-vs-sell exchange rates, and other fun ForEx business rules.

As a banking institution I want to pay interest on accounts based on their balance.
- Interest is accrued on an hourly basis
- Interest can be accrued in fractions of a cent.
- Interest is paid into an account each week
- Interest is only paid if it exceeds $0.01
- Pending interest payments for an account should not be visible to customers.
- Pending interest payments for an account should be visible to a bank manager
- Interest rates are fixed at 3.75%

[Extension] As a banking institution I want to retroactively apply interest rate changes.
- Payment of extra interest from an increased rate should be made immediately
- Recovery of overpaid interest from a reduced rate should be recovered from pending interest. I.e. the customer account balance should not be affected.

## Deposits

As someone interacting with the bank I want to deposit money into an account
- Deposits can be in the form of cash, personal cheques, or bank cheques.
- Anyone can make a deposit into an account.
- Deposits must be non-zero, positive amounts.
- Account balances should adjusted based on the transaction amount.
- Cheques are assumed to be cleared instantly [extension: cheques require 3 days before clearing, uncleared amounts should be reflected in the account balances]

As a regulatory body, I want all deposits above $10,000 to be flagged for review.
- Auditing logs should be available on a per-day basis
- Entries should show the transaction details.

## Withdrawals

As an account holder I want to withdraw funds from one of my accounts as cash
- Only allow withdrawals from a single account.
- Only non-zero positive amounts can be withdrawn.
- Accounts cannot be overdrawn.

[Extension] As a banking institution I want to allow customers to withdraw customers via a “virtual ATM”
- Login with a virtual ATM Card – a 16 digit number
- Enter a PIN number
- Withdraw cash in fixed amounts.

## Transfers

As an account holder I can transfer funds from my account to another account.
- The target account needs to be in the same banking institution (no cross-bank transfers)
- All transfers must be for non-zero, positive amounts.
- All transfers are assumed to clear instantly.
- Account balances in both accounts should be adjusted by the transferred amount

## Balances and Transactions:

As a customer I want to see an overview of my financial position
- Each account held by the customer should be shown, ordered by opening date
- The account name and current balance should be shown for each account
- Only accounts owned by the customer should be shown

As a bank manager I want to see an overview of my overall financial position
- Show the total number of customers and accounts
- Show the total funds held across all accounts, along with the average balance per account
- Show my top customers based on transaction volume in the last 30 days
- Show transaction volumes for the last 30 days (i.e. deposits, withdrawals, and transfers)

As an account holder I want to see the details and transaction history of a specific account
- Account details page, will display account name, account number, opening date, current balance and historical maximum balance
- Transaction history will show the details of each transaction, ordered by time in descending order.
- Transaction history should only show recent transactions (either a number of transactions or date based). Older transactions should be displayed on request.
- Customers should be able to search and filter their transaction history
- Each entry in the transaction history should include the date & time, source, value and balance after the transaction completed.
- Beside each transaction, user should be able to realize, if it’s withdraw or deposit.

[Extension] As an account holder I want to see live updates to my account balances and transaction history based when there is activity on my account.
- Refreshing the display can either be done via a notification and manual refresh, or automatically. The choice is yours.

# Technical notes

This exercise is intended to mimic building full systems. The goal is not to implement all of the rules that a full fledged banking system might require but to work through the different technologies – front end, bank end, communications, REST, security, events, and so on.

As can be seen from the technology list in the left column of the picture, this is a great chance to experiment and work with a range of technical options and choices.

More importantly though, we want the system to be built using a micro-services approach so that you think about some architectural concerns.  For example, how granular should those services be? It’s a design choice that you need to make. Similarly where do you draw the boundaries on the services? Do you denormalise data? Where does CQRS fit into the picture? Do you apply Domain Driven Design approaches? How much up front design do you do, or do you just jump in and code all the things? These are all choices that are up to you, much like a customer asking you for your approaches – just be ready to explain why you made those choices.

There are a few key items to note with the microservices approach in particualar:

1. Use an API gateway for your client application(s). Client applications should not be interacting with the microservices directly to reduce the chattiness of the application over the internet. Instead they communicate with an API optimized for their needs, and the gateway handles all the work needed to communicate with the other services.
2. How do you avoid the problems of 2-Phase Commit? Consider how an approach like event sourcing might help.
3. Microservices should be publishing state change events into a message bus or some other mechanism for cross service communications. Services should not directly communicate with each other and should not be aware of each other.
4. Microservices do not need to be built in a consistent manner. You do not have to use the same data storage approach or the same frameworks or languages. SQL and NoSQL data sources are welcomed.
5. Microservices can be deployed as IIS web sites, self-hosted services, be Azure Service Fabric based, or be separate docker images. Again – up to you how you do this. Recommendation: Start with the web site approach, and progress from there.
6. How you secure this is up to you, though we suggest the use of the IdentityServer open source project since it gets a fair amount of use.

# Definition of Done

It wouldn’t be right to have a project without a DoD. The following is the minimum bar.

1. All code should be in a git repository.
2. Code should be easy to maintain. Cyclomatic complexity should not exceed 20 for any method, and code analysis tools should report clean code (rules may be tweaked). This applies to all code, including JavaScript.
3. Code coverage should exceed 90%. If you take a test driven approach this should be achievable. The use of BDD style tests (Jasmine, SpecFlow, etc) is recommended, but up to you.
4. An automated build must exist and pass.
5. An automated deployment process must exist. This can be simple powershell scripts or something fancier such as Octopus or Visual Studio Release Manager.
6. A product backlog must be maintained and kept up to date. Some of the stories in the above backlog are fairly big – they should be decomposed if it makes sense to do so.
7. The system should be fault tolerant. If a microservice goes down the system should still function.

