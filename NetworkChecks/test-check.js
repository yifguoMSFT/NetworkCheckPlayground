"use strict"
require("debugFlow");

function Delay(second) {
    return new Promise(resolve =>
        setTimeout(resolve, second * 1000));
}

var resolveFunc;
var jsDynamicImportFlows = new Promise((resolve, reject) => {
    resolveFunc = resolve;
});

// load other scripts
function loadRemoteCheckAsync(name) {
    var promise = new Promise((resolve, reject) => {
        var existedScript = document.getElementById(name);
        if (existedScript != null) {
            document.head.removeChild(existedScript);
        }
        var script = document.createElement("script");
        //script.setAttribute('type', 'text/javascript');
        script.setAttribute('src', `http://127.0.0.1:8000/${name}.js`);
        script.setAttribute('id', name);
        //script.setAttribute('type', 'module');
        script.onload = () => {
            console.log(`remote debug script ${name}.js loaded!`);
            console.log(script);
            if (typeof debugFlows != 'undefined') {
                resolve(debugFlows);
            }
            else {
                resolve({});
            }
        }
        script.onerror = () => {
            resolve({});
        }
        document.head.appendChild(script);
    });
    return promise;
}
function require(name, isModule = false) {
    if(typeof requirePromises == 'undefined'){
        window["requirePromises"] = [];
    }
    var promise = new Promise((resolve, reject) => {
        var scriptName = "script-" + name;
        var existedScript = document.getElementById(scriptName);
        if (existedScript != null) {
            document.head.removeChild(existedScript);
        }
        var script = document.createElement("script");
        //script.setAttribute('type', 'text/javascript');
        script.setAttribute('src', `http://127.0.0.1:8000/${name}.js`);
        script.setAttribute('id', scriptName);
        if(isModule){
            script.setAttribute('type', 'module');
        }
        script.onload = () => {
            console.log(`remote debug script ${name}.js loaded!`);
            resolve();
        }
        script.onerror = e => {
            throw e;
        }
        document.head.appendChild(script);
    });
    requirePromises.push(promise);
    return promise;
}

Promise.all(requirePromises).then(()=> resolveFunc({ debugFlow }));
//loadRemoteCheckAsync("test-check-debug").then(val => resolveFunc({ ...flows, ...val }));
//resolveFunc(flows);

