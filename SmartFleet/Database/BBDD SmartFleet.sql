USE [SmartFleetDB];
GO

CREATE TABLE Empleado (
    IdEmpleado INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL,
    Passw NVARCHAR(100) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Apellidos VARCHAR(50) NOT NULL,
    Direccion VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20) NOT NULL,
    Email VARCHAR(50) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    FechaContratacion DATE NOT NULL,
    FotoPerfil IMAGE NULL
);
CREATE TABLE Vehiculo (
    IdVehiculo INT IDENTITY(1,1) PRIMARY KEY,
    Marca VARCHAR(50) NOT NULL,
    Modelo VARCHAR(50) NOT NULL,
    Matricula VARCHAR(10) NOT NULL,
    Kilometraje INT NOT NULL,
    Disponible BIT NOT NULL,
    Precio FLOAT NOT NULL,
    FotoVehiculo IMAGE NULL
);
CREATE TABLE Cliente (
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Apellidos NVARCHAR(50) NOT NULL,
    Correo NVARCHAR(50) NOT NULL UNIQUE,
    Telefono NVARCHAR(20) NOT NULL,
    Direccion NVARCHAR(100) NOT NULL,
    Ciudad NVARCHAR(50) NOT NULL,
    Provincia NVARCHAR(50) NOT NULL
);
CREATE TABLE Alquiler (
    IdAlquiler INT IDENTITY(1,1) PRIMARY KEY,
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    PrecioTotal FLOAT NOT NULL,
    IdCliente INT NOT NULL,
    IdVehiculo INT NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (IdVehiculo) REFERENCES Vehiculo(IdVehiculo)
);
CREATE TABLE Mantenimiento (
    IdMantenimiento INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATE NOT NULL,
    Descripcion VARCHAR(200) NOT NULL,
    Coste FLOAT NOT NULL,
    IdVehiculo INT NOT NULL,
    FOREIGN KEY (IdVehiculo) REFERENCES Vehiculo(IdVehiculo)
);


INSERT INTO Empleado (Username, Passw, Nombre, Apellidos, Direccion, Telefono, Email, FechaNacimiento, FechaContratacion)
VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'Administrador', 'Admin', 'Calle Admin', '+34 123456789', 'admin@admin.com', '01-01-1990', '01-01-2020');

INSERT INTO Empleado (Username, Passw, Nombre, Apellidos, Direccion, Telefono, Email, FechaNacimiento, FechaContratacion, FotoPerfil)
VALUES ('juan.perez', 'c15f3831e1a4a86046b22fc49f728fafe6e83f9782588dc1701ae5a1a971ef82', 'Juan', 'Pérez', 'Calle Principal 123', '+34 123456789', 'juan.perez@example.com', '1990-05-15', '2022-01-01', NULL);

INSERT INTO Empleado (Username, Passw, Nombre, Apellidos, Direccion, Telefono, Email, FechaNacimiento, FechaContratacion, FotoPerfil)
VALUES ('ana.lopez', '40b97b700617ada4afd92fb77bfce3e0e97e07da719b724c8034b39d4937adbf', 'Ana', 'López', 'Avenida Central 456', '+34 987654321', 'ana.lopez@example.com', '1992-09-20', '2022-02-15', NULL);

INSERT INTO Empleado (Username, Passw, Nombre, Apellidos, Direccion, Telefono, Email, FechaNacimiento, FechaContratacion, FotoPerfil)
VALUES ('carlos.gomez', 'e3145fdda2241fbbff39e5209d9e66f92a4adc7991002490e5b747cee22b1e98', 'Carlos', 'Gómez', 'Plaza Mayor 789', '+34 234567890', 'carlos.gomez@example.com', '1988-12-10', '2022-03-10', NULL);

INSERT INTO Empleado (Username, Passw, Nombre, Apellidos, Direccion, Telefono, Email, FechaNacimiento, FechaContratacion, FotoPerfil)
VALUES ('laura.martinez', 'b378bd35e148951ecc6bd2a565b9cfbf89ca261df2b07102c1184a5a9b1f3d6b', 'Laura', 'Martínez', 'Calle Secundaria 123', '+34 345678901', 'laura.martinez@example.com', '1994-03-25', '2022-04-05', NULL);

INSERT INTO Empleado (Username, Passw, Nombre, Apellidos, Direccion, Telefono, Email, FechaNacimiento, FechaContratacion, FotoPerfil)
VALUES ('david.fernandez', 'f481a9d7f05b6e570e3bf38e920c284a3a37a4ff0c8abe4c7e95208bbfd24467', 'David', 'Fernández', 'Avenida Principal 456', '+34 678901234', 'david.fernandez@example.com', '1991-08-12', '2022-05-20', NULL);

INSERT INTO Empleado (Username, Passw, Nombre, Apellidos, Direccion, Telefono, Email, FechaNacimiento, FechaContratacion, FotoPerfil)
VALUES ('sofia.rodriguez', '2066f1f291033b8fb1a682b865e3e16b2a5bbc289aeee4122b511f7ccbcdf502', 'Sofía', 'Rodríguez', 'Plaza Central 789', '+34 901234567', 'sofia.rodriguez@example.com', '1993-11-07', '2022-06-15', NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Honda', 'Civic', 'DEF456', 80000, 1, 12000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Chevrolet', 'Cruze', 'GHI789', 40000, 1, 13000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Nissan', 'Altima', 'JKL012', 60000, 1, 14000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Toyota', 'Camry', 'MNO345', 70000, 1, 16000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Ford', 'Escape', 'PQR678', 30000, 1, 18000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Honda', 'Accord', 'STU901', 50000, 1, 17000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Chevrolet', 'Malibu', 'VWX234', 90000, 1, 11000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Nissan', 'Sentra', 'YZA567', 60000, 1, 14000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Toyota', 'Rav4', 'BCD890', 40000, 1, 19000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Ford', 'Explorer', 'EFG123', 70000, 1, 25000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Honda', 'Pilot', 'HIJ456', 60000, 1, 23000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Chevrolet', 'Tahoe', 'KLM789', 80000, 1, 27000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Nissan', 'Maxima', 'NOP012', 50000, 1, 22000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Toyota', 'Highlander', 'QRS345', 90000, 1, 30000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Ford', 'F-150', 'TUV678', 10000, 1, 32000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Honda', 'CR-V', 'WXY901', 30000, 1, 28000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Chevrolet', 'Silverado', 'ZAB234', 20000, 1, 35000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Nissan', 'Pathfinder', 'BCD567', 40000, 1, 27000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Toyota', 'Sienna', 'EFG890', 60000, 1, 32000.00, NULL);

INSERT INTO Vehiculo (Marca, Modelo, Matricula, Kilometraje, Disponible, Precio, FotoVehiculo)
VALUES ('Ford', 'Expedition', 'HIJ123', 50000, 1, 37000.00, NULL);

INSERT INTO Cliente (Nombre, Apellidos, Correo, Telefono, Direccion, Ciudad, Provincia)
VALUES ('Juan', 'Gómez', 'juangomez@example.com', '+34 123456789', 'Calle Principal 123', 'Madrid', 'Madrid');

INSERT INTO Cliente (Nombre, Apellidos, Correo, Telefono, Direccion, Ciudad, Provincia)
VALUES ('María', 'López', 'marialopez@example.com', '+34 987654321', 'Avenida Central 456', 'Barcelona', 'Barcelona');

INSERT INTO Cliente (Nombre, Apellidos, Correo, Telefono, Direccion, Ciudad, Provincia)
VALUES ('Pedro', 'Fernández', 'pedrofernandez@example.com', '+34 234567890', 'Plaza Mayor 789', 'Valencia', 'Valencia');

INSERT INTO Cliente (Nombre, Apellidos, Correo, Telefono, Direccion, Ciudad, Provincia)
VALUES ('Ana', 'Martínez', 'anamartinez@example.com', '+34 567890123', 'Calle Secundaria 012', 'Sevilla', 'Sevilla');

INSERT INTO Cliente (Nombre, Apellidos, Correo, Telefono, Direccion, Ciudad, Provincia)
VALUES ('Luisa', 'García', 'luisagarcia@example.com', '+34 901234567', 'Avenida Principal 345', 'Zaragoza', 'Zaragoza');

INSERT INTO Cliente (Nombre, Apellidos, Correo, Telefono, Direccion, Ciudad, Provincia)
VALUES ('Carlos', 'Rodríguez', 'carlosrodriguez@example.com', '+34 234567890', 'Plaza Central 567', 'Málaga', 'Málaga');

INSERT INTO Cliente (Nombre, Apellidos, Correo, Telefono, Direccion, Ciudad, Provincia)
VALUES ('Laura', 'Gómez', 'lauragomez@example.com', '+34 678901234', 'Calle Principal 789', 'Bilbao', 'Vizcaya');

INSERT INTO Cliente (Nombre, Apellidos, Correo, Telefono, Direccion, Ciudad, Provincia)
VALUES ('Miguel', 'López', 'miguellopez@example.com', '+34 901234567', 'Avenida Central 901', 'Murcia', 'Murcia');

INSERT INTO Cliente (Nombre, Apellidos, Correo, Telefono, Direccion, Ciudad, Provincia)
VALUES ('Sofía', 'García', 'sofiagarcia@example.com', '+34 123456789', 'Plaza Mayor 012', 'Alicante', 'Alicante');

INSERT INTO Cliente (Nombre, Apellidos, Correo, Telefono, Direccion, Ciudad, Provincia)
VALUES ('David', 'Fernández', 'davidfernandez@example.com', '+34 567890123', 'Calle Secundaria 345', 'Córdoba', 'Córdoba');

INSERT INTO Alquiler (FechaInicio, FechaFin, PrecioTotal, IdCliente, IdVehiculo)
VALUES ('2023-01-01', '2023-01-05', 200.00, 1, 1);

INSERT INTO Alquiler (FechaInicio, FechaFin, PrecioTotal, IdCliente, IdVehiculo)
VALUES ('2023-02-10', '2023-02-15', 350.00, 2, 2);

INSERT INTO Alquiler (FechaInicio, FechaFin, PrecioTotal, IdCliente, IdVehiculo)
VALUES ('2023-03-20', '2023-03-25', 400.00, 3, 3);

INSERT INTO Alquiler (FechaInicio, FechaFin, PrecioTotal, IdCliente, IdVehiculo)
VALUES ('2023-04-05', '2023-04-10', 300.00, 4, 4);

INSERT INTO Alquiler (FechaInicio, FechaFin, PrecioTotal, IdCliente, IdVehiculo)
VALUES ('2023-05-10', '2023-05-15', 250.00, 5, 5);

INSERT INTO Alquiler (FechaInicio, FechaFin, PrecioTotal, IdCliente, IdVehiculo)
VALUES ('2023-06-20', '2023-06-25', 300.00, 6, 6);

INSERT INTO Alquiler (FechaInicio, FechaFin, PrecioTotal, IdCliente, IdVehiculo)
VALUES ('2023-07-05', '2023-07-10', 350.00, 7, 7);

INSERT INTO Alquiler (FechaInicio, FechaFin, PrecioTotal, IdCliente, IdVehiculo)
VALUES ('2023-08-10', '2023-08-15', 400.00, 8, 8);

INSERT INTO Alquiler (FechaInicio, FechaFin, PrecioTotal, IdCliente, IdVehiculo)
VALUES ('2023-09-20', '2023-09-25', 350.00, 9, 9);

INSERT INTO Alquiler (FechaInicio, FechaFin, PrecioTotal, IdCliente, IdVehiculo)
VALUES ('2023-10-05', '2023-10-10', 300.00, 10, 10);

INSERT INTO Mantenimiento (Fecha, Descripcion, Coste, IdVehiculo)
VALUES ('2023-01-10', 'Cambio de aceite', 50.00, 1);

INSERT INTO Mantenimiento (Fecha, Descripcion, Coste, IdVehiculo)
VALUES ('2023-02-15', 'Revisión de frenos', 80.00, 2);

INSERT INTO Mantenimiento (Fecha, Descripcion, Coste, IdVehiculo)
VALUES ('2023-03-20', 'Sustitución de neumáticos', 200.00, 3);

INSERT INTO Mantenimiento (Fecha, Descripcion, Coste, IdVehiculo)
VALUES ('2023-04-25', 'Limpieza interior y exterior', 30.00, 4);

INSERT INTO Mantenimiento (Fecha, Descripcion, Coste, IdVehiculo)
VALUES ('2023-05-30', 'Reparación de motor', 500.00, 5);

INSERT INTO Mantenimiento (Fecha, Descripcion, Coste, IdVehiculo)
VALUES ('2023-06-05', 'Cambio de filtro de aire', 40.00, 6);

INSERT INTO Mantenimiento (Fecha, Descripcion, Coste, IdVehiculo)
VALUES ('2023-07-10', 'Revisión de suspensión', 90.00, 7);

INSERT INTO Mantenimiento (Fecha, Descripcion, Coste, IdVehiculo)
VALUES ('2023-08-15', 'Alineación y balanceo de ruedas', 60.00, 8);

INSERT INTO Mantenimiento (Fecha, Descripcion, Coste, IdVehiculo)
VALUES ('2023-09-20', 'Cambio de batería', 120.00, 9);

INSERT INTO Mantenimiento (Fecha, Descripcion, Coste, IdVehiculo)
VALUES ('2023-10-25', 'Reparación de sistema eléctrico', 150.00, 10);
