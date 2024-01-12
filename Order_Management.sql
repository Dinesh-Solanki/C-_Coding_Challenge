CREATE DATABASE Order_Management;

USE Order_Management;

CREATE TABLE Product (
    productId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    productName VARCHAR(100) NOT NULL,
    description VARCHAR(100) NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    quantityInStock INT NOT NULL,
    type VARCHAR(50) CHECK (type IN ('Electronics', 'Clothing')) NOT NULL
);




CREATE TABLE Users(
 userId INT PRIMARY KEY IDENTITY(300,1),
 username VARCHAR(100) NOT NULL,
 password VARCHAR(100) NOT NULL,
 role VARCHAR(100) CHECK (role IN ('Admin', 'User')) NOT NULL
 );



 select * from product
 select * from users

CREATE TABLE Orders (
    orderId INT PRIMARY KEY IDENTITY(1,1),
    userId INT NOT NULL,
    productId INT NOT NULL,
    size VARCHAR(50),
    color VARCHAR(50),
    brand VARCHAR(255),
    warranty VARCHAR(255),
	date datetime not null,
    FOREIGN KEY (userId) REFERENCES Users(userId),
    FOREIGN KEY (productId) REFERENCES Product(productId)
);






