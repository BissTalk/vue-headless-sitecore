"use strict";
const LocalWebServer = require("local-web-server");
const localWebServer = new LocalWebServer();

var yarn = "yarn";

if (process.platform === "win32") {
    yarn = "yarn.cmd";
}

const spawn = require("child_process").spawn;
const build = spawn(yarn, ["run", "build"], { stdio: "inherit" });

build.on("close", (code) =>
{
    if (code === 0) {
        const server = localWebServer.listen({
            port: 3001,
            directory: "wwwroot",
            spa: "index.html",
            rewrite: [{ from: "/-/*", to: "http://sc812.local/-/$1" },
                { from: "/headless/*", to: "http://sc812.local/headless/$1" }
             ]
        });
        if (process.platform === "win32") {
            const rl = require("readline").createInterface({
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
        console.log("\nTo exit (Crtl-C)");
    }
});