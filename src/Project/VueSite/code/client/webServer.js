const LocalWebServer = require("local-web-server");
const localWebServer = new LocalWebServer();

var yarn = "yarn";

if (process.platform === "win32") {
    yarn = "yarn.cmd";
}

var spawn = require("child_process").spawn;
spawn(yarn, ["run", "build"], { stdio: "inherit" });

const server = localWebServer.listen({
    port: 3001,
    directory: "wwwroot",
    spa: "index.html",
    rewrite: [{ from: "/-/*", to: "http://sc812.local/-/$1" },
        { from: "/headless/*", to: "http://sc812.local/headless/$1" }
     ]
});

if (process.platform === "win32") {
    var rl = require("readline").createInterface({
        input: process.stdin,
        output: process.stdout
    });

    rl.on("SIGINT", function () {
        process.emit("SIGINT");
    });
}

process.on("SIGINT", function () {
    //graceful shutdown
    server.close();
    process.exit();
});

//server.close();

console.log("\nTo exit (Crtl-C)")