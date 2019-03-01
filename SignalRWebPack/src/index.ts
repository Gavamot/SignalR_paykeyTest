import "./css/main.css";
import * as signalR from "@aspnet/signalr";

const divMessages: HTMLDivElement = document.querySelector("#divMessages");
const tbMessage: HTMLInputElement = document.querySelector("#tbMessage");
const btnSend: HTMLButtonElement = document.querySelector("#btnSend");
const btnGetMessages: HTMLButtonElement = document.querySelector("#btnGetMessages");

const messages: HTMLPreElement = document.querySelector("#messages");
const username = new Date().getTime();

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

connection.start().catch(err => document.write(err));

connection.on("messageReceived", (username: string, datatime: Date, message: string) => {
    let messageContainer = document.createElement("div");

    messageContainer.innerHTML = `<div class="message-author">datatime:${datatime.toLocaleString()} | user:${username}</div><div>${message}</div><hr/>`;

    divMessages.appendChild(messageContainer);
    divMessages.scrollTop = divMessages.scrollHeight;
});

tbMessage.addEventListener("keyup", (e: KeyboardEvent) => {
    if (e.keyCode === 13) {
        send();
    }
});

btnSend.addEventListener("click", send);

function send() {
    var msg = tbMessage.value.trim();
    if (msg) {
        connection.send("newMessage", username, msg)
            .then(() => tbMessage.value = "");
    }
}

 async function getResource(url) {
     const result = await fetch(url);

     if (!result.ok) {
         console.error(`Could not ferch ${url}, receiver ${result.status}`);//throw new Error(`Could not ferch ${url}, receiver ${result.status}`)
    }

    const body = await result.json();
    return body;
};

async function GetMessages() {
    const data = await getResource("Message/GetAll");
    messages.textContent = data
        .map(msg => `${msg.dateTime} - user=${msg.user} said - ${msg.text}`)
        .join("\n\r");
}

btnGetMessages.addEventListener("click", () => {
    GetMessages();
}, false);

