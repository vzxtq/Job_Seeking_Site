-- Создание всех баз данных
CREATE DATABASE auth_db;
CREATE DATABASE job_db;
CREATE DATABASE application_db;

-- Подключение к базе job_db
\connect job_db

-- Включение расширения для генерации UUID
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- Таблица Jobs
CREATE TABLE Jobs (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Title TEXT NOT NULL,
    Description TEXT NOT NULL,
    Company TEXT NOT NULL,
    Location TEXT NOT NULL,
    EmploymentType TEXT NOT NULL,
    Salary DECIMAL(18, 2) NOT NULL,
    Requirements TEXT NOT NULL,
    Responsibilities TEXT NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
