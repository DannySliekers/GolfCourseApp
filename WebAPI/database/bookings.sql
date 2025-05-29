CREATE TABLE bookings (
    id SERIAL PRIMARY KEY,
    golf_course_id INT NOT NULL,
    created_by_user_id INT NOT NULL,
    start_time TIMESTAMPTZ NOT NULL,
    FOREIGN KEY (golf_course_id) REFERENCES golf_courses(id) ON DELETE CASCADE,
    FOREIGN KEY (created_by_user_id) REFERENCES users(id) ON DELETE CASCADE
);