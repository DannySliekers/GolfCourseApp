CREATE TABLE golf_courses (
    id SERIAL PRIMARY KEY,
    owner_id INT NOT NULL,
    name TEXT NOT NULL,
    description TEXT,
    longitude DECIMAL NOT NULL,
    latitude DECIMAL NOT NULL,
    booking_start_time TIME NOT NULL,
    booking_last_start_time TIME NOT NULL,
    start_time_interval_minutes SMALLINT NOT NULL,
    amount_of_holes SMALLINT NOT NULL,
    address TEXT NOT NULL,
    email TEXT NOT NULL,
    phone TEXT NOT NULL,
    FOREIGN KEY (owner_id) REFERENCES users(id) ON DELETE CASCADE
);