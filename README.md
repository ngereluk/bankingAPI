
# bankingAPI

  

The world's most basic bank. You can setup an account, a user, link an account to a user and send money between accounts.

  

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
This application is hosted at https://hammerhead-app-9ymyo.ondigitalocean.app/

  