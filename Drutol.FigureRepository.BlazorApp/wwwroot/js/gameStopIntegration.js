//export function hasGameStop() {
//    return (window.gamestop != undefined);
//}

//export function isSiteConnected() {
//    return (window.gamestop != undefined && (gamestop.selectedAddress != undefined || gamestop.selectedAddress != null));
//}

export async function checkGameStop() {
    // Modern dapp browsers...
    if (window.gamestop) {

        try {
            // Request account access if needed
            await requestAccounts();
        } catch (error) {
            // User denied account access...
            throw "UserDenied"
        }
        
        console.log("Selected:" + gamestop.currentAddress);
       
    }
    // Non-dapp browsers...
    else {
        throw "NoGameStop"
    }
}

export async function requestAccounts() {
    console.log('reqAccount');
    var result = await gamestop.request({
        method: 'eth_requestAccounts',
    });
    return result;
}

export function hasGameStop() {
    return (window.gamestop != undefined);
}

export function isSiteConnected() {
    return (window.gamestop != undefined && (gamestop.currentAddress != undefined || gamestop.currentAddress != null));
}

export async function getSelectedAddress() {
    await checkGameStop();

    return gamestop.currentAddress;
}

export async function listenToChangeEvents() {
    if (hasGameStop()) {
        //gamestop.on("connect", function () {
        //    DotNet.invokeMethodAsync('GameStop.Blazor', 'OnConnect');
        //});

        //gamestop.on("disconnect", function () {
        //    DotNet.invokeMethodAsync('GameStop.Blazor', 'OnDisconnect');
        //});

        gamestop.on("accountsChanged", function (accounts) {
            DotNet.invokeMethodAsync('Drutol.FigureRepository.BlazorApp', 'OnAccountsChanged', accounts[0]);
        });

        gamestop.on("chainChanged", function (chainId) {
            DotNet.invokeMethodAsync('Drutol.FigureRepository.BlazorApp', 'OnChainChanged', chainId);
        });
    }
}

export async function getSelectedChain() {
    await checkGameStop();

    var result = await gamestop.request({
        method: 'eth_chainId'
    });
    return result;
}

export async function getTransactionCount() {
    await checkGameStop();

    var result = await gamestop.request({
        method: 'eth_getTransactionCount',
        params:
            [
                gamestop.selectedAddress,
                'latest'
            ]

    });
    return result;
}

export async function signTypedData(label, value) {
    await checkGameStop();

    const msgParams = [
        {
            type: 'string', // Valid solidity type
            name: label,
            value: value
        }
    ]

    try {
        var result = await gamestop.request({
            method: 'eth_signTypedData',
            params:
                [
                    msgParams,
                    gamestop.selectedAddress
                ]
        });

        return result;
    } catch (error) {
        // User denied account access...
        throw "UserDenied"
    }
}

export async function signTypedDataV4(typedData) {
    await checkGameStop();

    try {
        var result = await gamestop.request({
            method: 'eth_signTypedData_v4',
            params:
                [
                    gamestop.selectedAddress,
                    typedData
                ],
            from: gamestop.selectedAddress
        });

        return result;
    } catch (error) {
        // User denied account access...
        throw "UserDenied"
    }
}

export async function sendTransaction(to, value, data) {
    await checkGameStop();

    const transactionParameters = {
        to: to,
        from: gamestop.selectedAddress, // must match user's active address.
        value: value,
        data: data
    };

    try {
        var result = await gamestop.request({
            method: 'eth_sendTransaction',
            params: [transactionParameters]
        });

        return result;
    } catch (error) {
        if (error.code == 4001) {
            throw "UserDenied"
        }
        throw error;
    }
}

export async function genericRpc(method, params) {
    await checkGameStop();

    var result = await gamestop.request({
        method: method,
        params: params
    });

    return result;
}