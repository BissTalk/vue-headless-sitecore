/// <binding BeforeBuild='Run - Production' />
"use strict";
const path = require("path");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const CleanWebpackPlugin = require("clean-webpack-plugin");
const UglifyJsPlugin = require("uglifyjs-webpack-plugin");
const ouputDir = "wwwroot/dist";

module.exports = [
    {
        entry: "./client/boot.js",
        resolve: {
            extensions: [".js", ".vue"],
            alias: {
                'vue$': "vue/dist/vue",
                'components': path.resolve(__dirname, "./client/components")
            }
        },
        output: {
            filename: "app.min.js",
            path: path.resolve(__dirname, ouputDir)
        },
        module: {
            rules: [
                {
                    test: /\.(scss)$/,
                    use: [
                        {
                            loader: "style-loader", // inject CSS to page
                        }, {
                            loader: "css-loader", // translates CSS into CommonJS modules
                        }, {
                            loader: "postcss-loader", // Run post css actions
                            options: {
                                plugins: function() { // post css plugins, can be exported to postcss.config.js
                                    return [
                                        require("precss"),
                                        require("autoprefixer")
                                    ];
                                }
                            }
                        }, {
                            loader: "sass-loader" // compiles Sass to CSS
                        }
                    ]
                },
                {
                    test: /\.css$/,
                    use: ExtractTextPlugin.extract({
                        fallback: "style-loader",
                        use: {
                            loader: "css-loader" //,
                            //options: { minimize: true }
                        }
                    })
                },
                {
                    test: /\.js$/,
                    include: /client/,
                    use: {
                        loader: "babel-loader",
                        query: {
                            presets: ["es2015", "stage-2"]
                        }
                    }
                },
                { test: /\.vue$/, use: "vue-loader" },
                { test: /\.(png|jpg|jpeg|gif|svg)$/, use: "url-loader?limit=25000" },
                { test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/, loader: "url-loader?limit=10000&mimetype=application/font-woff" },
                { test: /\.(ttf|eot|svg)(\?v=[0-9]\.[0-9]\.[0-9])?$/, loader: "file-loader" }
            ]
        },
        plugins: [
            new CleanWebpackPlugin([ouputDir]),
            new ExtractTextPlugin({
                filename: "style.min.css",
                disable: false,
                allChunks: true
            }) //,
            // new UglifyJsPlugin()
        ]
    }
];