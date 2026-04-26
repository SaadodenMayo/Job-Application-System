const mysql = require('mysql2');

const db = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: '', 
    database: 'job_application_db'
});

// Mao ni ang mag-print sa terminal
db.connect((err) => {
    if (err) {
        console.log("❌ Database connection failed!");
    } else {
        console.log("✅ Connected to MySQL Database!");
    }
});

module.exports = db;