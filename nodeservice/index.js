// index.js

const express = require("express");
const app = express();
const port = 3000;

app.get("/hello", (req, res) => {
  res.send({ message: "Hello from Microservice 2!" });
});

app.listen(port, () => {
  console.log(`Microservice 2 listening at http://localhost:${port}`);
});
