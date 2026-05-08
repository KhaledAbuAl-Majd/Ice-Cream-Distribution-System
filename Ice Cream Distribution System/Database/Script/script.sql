-- 1. Create Database
CREATE DATABASE IceCreamDistributionDB;
GO
USE IceCreamDistributionDB;
GO

-- 2. Basic Tables (Lookup Tables)
CREATE TABLE Areas (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(150) NOT NULL,
    Notes NVARCHAR(500)
);

CREATE TABLE ProductTypes (
    ID SMALLINT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(150) NOT NULL
);

CREATE TABLE People (
    PersonID INT PRIMARY KEY IDENTITY(1,1),
    PersonName NVARCHAR(200) NOT NULL,
    Address NVARCHAR(300) NULL,
    Email NVARCHAR(255) NULL,
    Phone VARCHAR(20) NULL
);

-- 3. Inventory & Assets
CREATE TABLE Stores (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Balance DECIMAL(18,4) DEFAULT 0,
    AreaID INT REFERENCES Areas(ID),
    OwnerID INT REFERENCES People(PersonID)
);

CREATE TABLE Cars (
    ID SMALLINT PRIMARY KEY IDENTITY(1,1),
    AreaID INT REFERENCES Areas(ID),
    CarDetails NVARCHAR(500) NULL
);

CREATE TABLE Products (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(150) NOT NULL,
    ProductTypeID SMALLINT REFERENCES ProductTypes(ID),
    Price DECIMAL(18,4) NOT NULL
);

-- 4. User Management (Modified as per request)
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50) UNIQUE NOT NULL,
    PersonID INT UNIQUE REFERENCES People(PersonID),
    PasswordHash NVARCHAR(100) NOT NULL, -- For BCrypt
    IsActive BIT DEFAULT 1,
    AccountID TINYINT,
    IsDeleted BIT DEFAULT 0
);

-- 5. Staff & Field Workers
CREATE TABLE Representatives (
    ID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT UNIQUE REFERENCES Users(UserID),
    CarID SMALLINT REFERENCES Cars(ID)
);

CREATE TABLE Drivers (
    ID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT UNIQUE REFERENCES Users(UserID),
    CarID SMALLINT REFERENCES Cars(ID)
);

-- 6. Operations (Shifts & Invoices)
CREATE TABLE Shifts (
    ID INT PRIMARY KEY IDENTITY(1,1),
    FromDate DATETIME2 NOT NULL,
    ToDate DATETIME2 NULL,
    RepresentativeID INT REFERENCES Representatives(ID),
    DriverID INT REFERENCES Drivers(ID),
    CarID SMALLINT REFERENCES Cars(ID)
);

CREATE TABLE Invoices (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Date DATETIME2 DEFAULT GETDATE(),
    CarID SMALLINT REFERENCES Cars(ID),
    StoreID INT REFERENCES Stores(ID),
    Notes NVARCHAR(250) NULL,
    Total DECIMAL(18,4) DEFAULT 0
);

CREATE TABLE InvoiceRecords (
    ID INT PRIMARY KEY IDENTITY(1,1),
    InvoiceID INT REFERENCES Invoices(ID) ON DELETE CASCADE,
    ProductID INT REFERENCES Products(ID),
    Count SMALLINT NOT NULL,
    ProductPrice DECIMAL(18,4) NOT NULL,
    Total AS (Count * ProductPrice) PERSISTED -- Computed Column
);

-- 7. Finances & Tracking
CREATE TABLE Payments (
    ID INT PRIMARY KEY IDENTITY(1,1),
    PayedValue DECIMAL(18,4) NOT NULL,
    Date DATETIME2 DEFAULT GETDATE(),
    RepresentativeID INT REFERENCES Representatives(ID),
    StoreID INT REFERENCES Stores(ID),
    Notes NVARCHAR(250) NULL
);

CREATE TABLE RepresentativesStock (
    ID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT REFERENCES Products(ID),
    RepresentativeID INT REFERENCES Representatives(ID),
    Count SMALLINT DEFAULT 0,
);

-- 8. Performance Indexes
CREATE INDEX IX_Invoices_Date ON Invoices(Date);
CREATE INDEX IX_Payments_Representative ON Payments(RepresentativeID);
CREATE INDEX IX_Shifts_Dates ON Shifts(FromDate, ToDate);
CREATE INDEX IX_Products_Name ON Products(Name);
CREATE UNIQUE INDEX IX_Users_UserName ON Users(UserName);

CREATE UNIQUE INDEX IX_Users_PersonID ON Users(PersonID) WHERE IsDeleted =0;

-- 2. تسريع الـ Joins في الفواتير (أهم جزء في التوزيع)
CREATE INDEX IX_Invoices_Car_Store ON Invoices(CarID, StoreID); 

-- 3. تسريع البحث عن أصناف فاتورة معينة
CREATE INDEX IX_InvoiceRecords_InvoiceID ON InvoiceRecords(InvoiceID);

-- 4. تسريع معرفة رصيد مندوب معين في المخزن
CREATE INDEX IX_RepStock_Rep_Product ON RepresentativesStock(RepresentativeID, ProductID);

-- 5. تسريع البحث عن العمليات في منطقة معينة
CREATE INDEX IX_Stores_Area ON Stores(AreaID);