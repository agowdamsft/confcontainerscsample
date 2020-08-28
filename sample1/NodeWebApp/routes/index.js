var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Lamna Hospital Portal' });
  
});
//sample URI, Need to be replaced with actual web api
router.post('https://reqres.in/api/users', function (req, res) {
  req.
  console.log(req.body.title);
  console.log(req.body.description);
  res.send({"name":"test", "job":"big by" });
});

module.exports = router;
