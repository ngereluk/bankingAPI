
# bankingAPI

  

The world's most basic bank. You can setup an account, a user, link an account to a user and send money between accounts. If you're interested in trying out the API, scroll halfway down to see a suggested workflow of cURL commands.

  

# API Spec

## Account

### GET /api/account

Returns an array of all accounts:

	[
	 {
	"id": "string",
	"accountBalance": long,
	"createdAt": DateTime,
	"updatedAt": DateTime,
	"deleted": bool,
	"deletedAt": DateTime,
	 }
	]
  

### POST /api/account

Creates a new account. The body of the request should contain the account balance only:
		```
	{
	  "accountBalance": 20
	}
	```

### GET /api/account/{id}
Gets a single account object by id. Returns the account object:
  ```
{
  "id": "string",
  "accountBalance": 0,
"accountBalance": long,
"createdAt": DateTime,
"updatedAt": DateTime,
"deleted": bool,
"deletedAt": DateTime,
}
```

### PUT /api/account/{id}
Can be used to update the account balance. Accepts the account id as a url path parameter and the new account balance in the body of the request:
  ```
{"accountBalance": 99999}
```

## Transactions
### GET /api/transaction
Returns an array of all transactions:
```
[
  {
    "id": "string",
    "amount": long,
    "senderUserId": "string",
    "senderAccountId": "string",
    "recipientAccountId": "string",
    "timeSent": DateTime
  }
]
```
### POST /api/transaction
Creates a new transaction. Accepts a body:
```
{
  "amount": long,
  "senderUserId": "string",
  "senderAccountId": "string",
  "recipientAccountId": "string"
}
```
### GET /api/transaction/{id}
Gets a single transaction object by id. Returns the transaction object:
```
{
  "id": "string",
  "amount": long,
  "senderUserId": "string",
  "senderAccountId": "string",
  "recipientAccountId": "string",
  "timeSent": DateTime
}
```

## User
### GET /api/user
Returns an array of all users:
```
[
  {
    "id": "string",
    "firstName": "Ann",
    "lastName": "Perkins",
    "addressLine1": "string",
    "addressLine2": "string",
    "postalCode": "string",
    "city": "string",
    "province": "string",
    "dob": "string",
    "accountIds": [
        "string"
    ],
    "createdAt": DateTime,
    "updatedAt": DateTime,
    "deleted": boolean,
    "deletedAt": DateTime
  },
]
```
### POST /api/user
Creates a new account. Accepts a body:
```
{
  "firstName": "string",
  "lastName": "string",
  "addressLine1": "string",
  "addressLine2": "string",
  "postalCode": "string",
  "city": "string",
  "province": "string",
  "dob": "string",
  "accountIds": [
    "string"
  ]
}
```
### GET /api/user/{id}
Gets a single user object by id. Returns the user object:
```
{
  "id": "string",
  "firstName": "string",
  "lastName": "string",
  "addressLine1": "string",
  "addressLine2": "string",
  "postalCode": "string",
  "city": "string",
  "province": "string",
  "dob": "string",
  "accountIds": [
    "string"
  ],
  "createdAt": DateTime,
  "updatedAt": DateTime,
  "deleted": boolean,
  "deletedAt": DateTime
}
```
### PUT /api/user/{id}
Can be used to update the user. Accepts the user id as a url path parameter and the following in the request body:
```
{
  "firstName": "string",
  "lastName": "string",
  "addressLine1": "string",
  "addressLine2": "string",
  "postalCode": "string",
  "city": "string",
  "province": "string",
  "dob": "string",
  "accountIds": [
    "string"
  ]
}
```

# Production App

~This application is hosted at https://hammerhead-app-9ymyo.ondigitalocean.app/.~. This app is no longer hosted. Here is a suggested workflow:

1. Create a new account with a balance of $99
```
cURL -X POST -d '{"accountBalance": 99}' -H "Content-Type: application/json" https://hammerhead-app-9ymyo.ondigitalocean.app/api/account
```
Send a get request to ensure your account was created (note you will have to find your account in a list of accounts)
```
curl https://hammerhead-app-9ymyo.ondigitalocean.app/api/account
```

2. The server response from the curl command in step 1 will include the account id in the id field. Take note of this id.

Create a new user and link them to the account created in step 1 using the account id. Note we are not including the optional address fields in our request.

```
cURL -X POST -d '{"firstName": "Tom","lastName": "Haverford","dob": "15/02/1984","accountIds": [<Account id from Step 1 as a string>]}' -H "Content-Type: application/json" https://hammerhead-app-9ymyo.ondigitalocean.app/api/user
```
Take note of the user id. Send a get request to ensure your user was created (note you will have to find your account in a list of accounts)
```
curl https://hammerhead-app-9ymyo.ondigitalocean.app/api/account
```
3. Create a second account with a balance of 5$ and take note if the account id.
```
cURL -X POST -d '{"accountBalance": 5}' -H "Content-Type: application/json" https://hammerhead-app-9ymyo.ondigitalocean.app/api/account
```
4. Create a transaction that sends 20$ from the first account to the second
 ```
cURL -X POST -d '{"amount": 20,"senderUserId": "<user id from step 2>","senderAccountId": "<account id from step 1>","recipientAccountId": "<account id from step 3>"}' -H "Content-Type: application/json" https://hammerhead-app-9ymyo.ondigitalocean.app/api/transaction
```
This transaction sends 20$ from the first account we created to the second.

5. Verify that 20$ was taken from the first account (the new balance should be 79$) 
```
curl https://hammerhead-app-9ymyo.ondigitalocean.app/api/account/<account id from step 1>
```
Verify that 20$ was added to the second account (the new balance should be 25$)
```
curl https://hammerhead-app-9ymyo.ondigitalocean.app/api/account/<account id from step 3>
```

  
