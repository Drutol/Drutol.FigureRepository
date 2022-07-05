function openPayPal(objReference, order) {
    paypal.Buttons({
        // Sets up the transaction when a payment button is clicked
        createOrder: (data, actions) => {
            return order;
        },
        // Finalize the transaction after payer approval
        onApprove: (data, actions) => {
            objReference.invokeMethodAsync('SubmitPaypalOrder', order);
        }
    }).render('#paypal-buttons');
}

globalThis.openPayPal = openPayPal;