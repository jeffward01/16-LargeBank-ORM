SELECT * FROM Customer;
Select CustomerId FROM Accounts WHERE Balance > 5000;
SELECT * FROM Customer WHERE (SELECT Sum(Balance) FROM Accounts WHERE Accounts.CustomerId = Customer.CustomerId) > 50000;

select * from Accounts;