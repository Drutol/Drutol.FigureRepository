﻿function openPayPal(objReference, order) {
    paypal.Buttons({
        // Sets up the transaction when a payment button is clicked
        createOrder: (data, actions) => {
            return order;
        },
        onInit: function (data, actions) {
            objReference.invokeMethodAsync('PaypalButtonsInitialized');
        },
        // Finalize the transaction after payer approval
        onApprove: (data, actions) => {
            objReference.invokeMethodAsync('SubmitPaypalOrder', order);
        }
    }).render('#paypal-buttons');
}

globalThis.openPayPal = openPayPal;