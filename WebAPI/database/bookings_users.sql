CREATE TABLE bookings_users (
    booking_id INT NOT NULL,
    user_id INT NOT NULL,
    PRIMARY KEY (booking_id, user_id),
    FOREIGN KEY (booking_id) REFERENCES bookings(id) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);