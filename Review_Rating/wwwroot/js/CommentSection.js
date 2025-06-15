"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/commentHub").build();

document.getElementById("postComment").disabled = true;


connection.on("ReceiveComment", function (user, statement) {
    var el = document.createElement("p");
    document.getElementById("commentList").appendChild(el);
    el.textContent = `${user}: ${statement}`;
});


connection.start().then(function () {
    document.getElementById("postComment").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});




document.getElementById("postComment").addEventListener("click", function (event) {
    var content = document.getElementById("contentId").value;
    var statement = document.getElementById("commentStatement").value;
    connection.invoke("PostComment", content, statement).catch(function (err) { return console.error(err.toString()); });
    event.preventDefault();
});