CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    email VARCHAR(255) NOT NULL UNIQUE,
    user_name VARCHAR(100) NOT NULL,
    hash TEXT NOT NULL,
    role VARCHAR(20) NOT NULL CHECK (role IN ('player', 'manager', 'admin')),
    avatar_url TEXT
);