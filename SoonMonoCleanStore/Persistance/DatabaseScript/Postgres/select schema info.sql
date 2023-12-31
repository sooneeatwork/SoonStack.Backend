SELECT table_name
FROM information_schema.tables
WHERE table_schema = 'auth';


SELECT table_name, column_name, data_type
FROM information_schema.columns
WHERE table_schema = 'auth' -- Replace with your schema name
ORDER BY table_name, ordinal_position;
