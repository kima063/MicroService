const mysql = require('mysql');
const express = require('express');
var app = express();
const bodyparser = require('body-parser');
var exeCount=0;
app.use(bodyparser.urlencoded({ extended: false }));
app.use(bodyparser.json());
const axios = require('axios')


var mysqlConnection =mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: '',
    database: 'ratingservicedb',
    multipleStatements: true
});
var request = require('request');
const { response } = require('express');

mysqlConnection.connect((err) => {
    if(!err)
        console.log('DB connected');
    else
        console.log('DB connection failed');    
});

app.listen(3000, () => console.log('Express server is runnig at port no : 3000'));

//Get all ratings
app.get('/rate', (req, res) => {
    console.log(exeCount);

    mysqlConnection.query('SELECT * FROM rating', (err, rows, fields) => {
        if (!err)
            res.send(rows);
        else
            console.log(err);
    })
});

//Get an employees
app.get('/rate/:id', (req, res) => {
    mysqlConnection.query('SELECT * FROM rating WHERE ratingID = ?', [req.params.id], (err, rows, fields) => {
        if (!err)
            res.send(rows);
        else
            console.log(err);
    })
});    

//Delete an employees
app.delete('/rate/:id', (req, res) => {
    mysqlConnection.query('DELETE FROM rating WHERE ratingID = ?', [req.params.id], (err, rows, fields) => {
        if (!err)
            res.send('Deleted successfully.');
        else
            console.log(err);
    })
});

//Insert an employees
app.post('/rate', (req, res) => {
    //let rating = req.body;
    exeCount=exeCount+1;
    console.log(exeCount);
    let emp = req.body;
    var sql = "SET @productId = ?;SET @rating = ?;SET @raterId = ?; \
    CALL store_rating(@productId,@rating,@raterId);";
    mysqlConnection.query(sql, [ emp.productId, emp.rating, emp.raterId], (err, rows, fields) => {
        //console.log("rows"+rows);
        if (!err){
            rows.forEach(element => {
            //    if(element.constructor == Array)
            //    res.send('Inserted rating product id : '+element[0].productId);
            });
            
        }
        else
            console.log(err);


    })
//    if(exeCount>=2)
           // {
                let rat = req.body;
                var sql2="USE ratingservicedb;\
                 SELECT productId,AVG(rating) as average,COUNT(productId) as ratingCount FROM rating GROUP BY productId;"
                 mysqlConnection.query(sql2, (err, rows) => {
                     //res.send(result);
                    //console.log("ekhane"+result);
                     if(!err){
                         if(exeCount>=5){
                            exeCount=0;
                            var Arr=rows[1]
                        
                            Arr.forEach(item=> {
                                console.log("item"+item.AVERAGE)
                                axios
                                .post('http://localhost:50682/product/updateRating', {
                                    "id":item.productId,
                                    "averageRating":item.average,
                                    "numberOfRaters":item.ratingCount
                                })
                                .then(res => {
                                   // console.log(`statusCode: ${res.statusCode}`)
                                   // console.log(res)
                                })
                                .catch(error => {
                                    console.error(error)
                                })
                                })
                         }                    
                              res.send(rows[1])

                         
                         //updateClient(result);
                         //console.log(result);
                        //  res.status(200).json({
                        //      statusCode:201,
                        //      error: false,
                        //      msg: "Updated",
                        //  });           
                     }else console.log(err);
                }
                 );
           // }
    //else{
    
    //}
    
});

