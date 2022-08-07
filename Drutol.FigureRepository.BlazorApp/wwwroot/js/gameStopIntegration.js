export function hasGameStop() {
    return (window.gamestop != undefined);
}

export function isSiteConnected() {
    return (window.gamestop != undefined && (gamestop.selectedAddress != undefined || gamestop.selectedAddress != null));
}