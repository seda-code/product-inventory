// http://thecodebarbarian.com/github-oauth-login-with-node-js.html

const express = require('express');
const cors = require('cors');
const axios = require('axios');
const app = express();

const DEV_URL = "http://localhost:5500/src/Inventory.Web";
const PRO_URL = "http://localhost";

app.use(cors());

var whiteList = ["http://localhost:5500"];
var corsOpt = {
  origin: function(origin, callback){
    if(!origin || whiteList.indexOf(origin) !== -1){
      callback(null, true);
    }else{
      callback(new Error(`Not allowed by CORS - ${origin}`));
    }
  }
}

let token = null;

const clientId = 'XXXXXXXXXXXXXXXX';
const clientSecret = 'XXXXXXXXXXXXXXXX';

app.get('/', cors(corsOpt), (req, res) => {
    res.redirect(`https://github.com/login/oauth/authorize?client_id=${clientId}&scope=user`);
  });

app.get('/oauth-callback', cors(corsOpt), (req, res) => {
  const body = {
    client_id: clientId,
    client_secret: clientSecret,
    code: req.query.code
  };

  console.log("Code: ", req.query.code);
  
  const opts = { headers: { accept: 'application/json' } };

  axios.post(`https://github.com/login/oauth/access_token`, body, opts)
  .then(res => res.data['access_token'])
  .then(_token => {
      console.log('My token:', _token);
      token = _token;
  })
  .then(()=>res.redirect("/info"))
  .catch(err => res.status(500).json({ message: err.message }));
});

app.get("/info", (req, res)=>{
  const opts = { 
    headers: { 
      accept: "application/vnd.github.v3+json",
      authorization: "token " + token
    } 
  };
  
  axios.get("https://api.github.com/user", opts)
  .then(res=> res.data)
  .then(info => {
    console.log(info["name"]);

    // res.json({name: info["name"]});

    const url = PRO_URL + `/?user=${info["name"]}`;
    res.redirect(url);
  })
  .catch(err => res.status(500).json({message: err}));
  
})

app.listen(3000);
console.log('Proxy listening on port 3000');

