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

UPDATE users set manager=1 where id=3;

exec display_all_tables;

delete from users;

-- DELETE FROM users;
DELETE FROM tickets;
