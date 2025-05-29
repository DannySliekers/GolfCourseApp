CREATE TABLE golf_course_images (
    id SERIAL PRIMARY KEY,
    golf_course_id INT NOT NULL,
    url TEXT NOT NULL,
    FOREIGN KEY (golf_course_id) REFERENCES golf_courses(id) ON DELETE CASCADE
);