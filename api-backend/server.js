const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');
const db = require('./config/db'); // Siguraduha nga naay db.connect() sa imong db.js
const swaggerUi = require('swagger-ui-express');
const swaggerDocument = require('./swagger.json'); 

const app = express();
app.use(cors());
app.use(bodyParser.json());

// --- SWAGGER SETUP ---
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocument));

// --- API ENDPOINTS ---

// 1. REGISTER / SIGN UP
app.post('/api/register', (req, res) => {
    const { username, password, email } = req.body;
    const sql = "INSERT INTO users (username, password, email, role) VALUES (?, ?, ?, 'Applicant')";
    db.query(sql, [username, password, email], (err, result) => {
        if (err) {
            console.error(err);
            return res.status(500).json({ success: false, error: err.message });
        }
        res.json({ success: true, message: "Account created successfully!" });
    });
});

// 2. LOGIN
app.post('/api/login', (req, res) => {
    const { username, password } = req.body;
    const sql = "SELECT * FROM users WHERE username = ? AND password = ?";
    db.query(sql, [username, password], (err, results) => {
        if (err) return res.status(500).json({ success: false, error: err.message });
        
        if (results.length > 0) {
            res.json({ success: true, user: results[0] });
        } else {
            res.status(401).json({ success: false, message: 'Invalid credentials' });
        }
    });
});

// 3. GET ALL JOBS
app.get('/api/jobs', (req, res) => {
    const sql = "SELECT * FROM jobs";
    db.query(sql, (err, results) => {
        if (err) return res.status(500).json({ error: err.message });
        res.json(results);
    });
});

// 4. POST A NEW JOB (Updated match sa imong database: job_title, job_description, status)
app.post('/api/jobs', (req, res) => {
    const { job_title, job_description } = req.body;
    
    // Siguraduha nga walay 'email' o 'salary' diri sa VALUES
    const sql = "INSERT INTO jobs (job_title, job_description, status) VALUES (?, ?, 'Open')";
    
    db.query(sql, [job_title, job_description], (err, result) => {
        if (err) {
            console.error("SQL Error sa Terminal:", err);
            return res.status(500).json({ success: false, error: err.message });
        }
        res.json({ success: true, message: "Job posted successfully!", id: result.insertId });
    });
});

// 5. SUBMIT APPLICATION (POST)
app.post('/api/applicants', (req, res) => {
    const { 
        job_id, full_name, email, contact_no, school_institution, 
        degree, year_completed, skills_qualification, application_status 
    } = req.body;
    
    const sql = `INSERT INTO applicants 
        (job_id, full_name, email, contact_no, school_institution, degree, year_completed, skills_qualification, application_status) 
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)`;

    db.query(sql, [
        job_id, full_name, email, contact_no, school_institution, 
        degree, year_completed, skills_qualification, application_status
    ], (err, result) => {
        if (err) return res.status(500).json({ error: err.message });
        res.json({ success: true, message: 'Application recorded!', id: result.insertId });
    });
});

// 6. GET ALL APPLICANTS (For Admin View)
app.get('/api/applicants', (req, res) => {
    const sql = "SELECT * FROM applicants";
    db.query(sql, (err, results) => {
        if (err) return res.status(500).json({ error: err.message });
        res.json(results);
    });
});

// 7. UPDATE APPLICANT STATUS
app.put('/api/applicants/:id', (req, res) => {
    const { id } = req.params;
    const { application_status } = req.body;
    const sql = "UPDATE applicants SET application_status = ? WHERE applicant_id = ?";
    db.query(sql, [application_status, id], (err, result) => {
        if (err) return res.status(500).json({ error: err.message });
        res.json({ message: 'Status updated!' });
    });
});

// 8. DELETE APPLICANT
app.delete('/api/applicants/:id', (req, res) => {
    const { id } = req.params;
    const sql = "DELETE FROM applicants WHERE applicant_id = ?";
    db.query(sql, [id], (err, result) => {
        if (err) return res.status(500).json({ error: err.message });
        res.json({ message: 'Record deleted!' });
    });
});

const PORT = 3000;
app.listen(PORT, () => {
    console.log(`\n🚀 API Server running on http://localhost:${PORT}`);
    console.log(`📖 Swagger Documentation: http://localhost:${PORT}/api-docs\n`);
});