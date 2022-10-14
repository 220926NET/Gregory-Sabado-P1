-- CREATE TABLE users
-- (
--     id INT PRIMARY KEY IDENTITY,
--     email NVARCHAR(MAX) NOT NULL,
--     username NVARCHAR(MAX),
--     password_hash NVARCHAR(MAX) NOT NULL,
--     user_position BIT DEFAULT 0
-- );

-- alter TABLE users
-- set


-- CREATE TABLE tickets
-- (
--     [id] int IDENTITY,
--     owner_id int,
--     [status] int CHECK([status] >= 0 and [status] <=3),
--     [amount] money CHECK(amount > 0),
--     [desc] NVARCHAR(max),
--     PRIMARY KEY(id),
--     foreign key(owner_id) REFERENCES users(id),
-- )

-- create PROCEDURE display_all_tables
-- as
-- SELECT *
-- FROM users
-- select *
-- from tickets
-- GO;

-- drop PROCEDURE display_all_tables

exec display_all_tables;