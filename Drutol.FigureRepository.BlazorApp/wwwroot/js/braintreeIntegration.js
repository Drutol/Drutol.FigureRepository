
function openPayPal(objReference, clientToken, amount) {
    paypal.Button.render({
        braintree: braintree,
        client: {
            production: clientToken,
            sandbox: clientToken
        },
        env: 'sandbox', // Or 'sandbox'
        commit: true, // This will add the transaction amount to the PayPal button

        payment: function (data, actions) {
            return actions.braintree.create({
                flow: 'checkout', // Required
                amount: amount, // Required
                currency: 'USD', // Required
                enableShippingAddress: false
            });
        },

        onAuthorize: function (payload) {
            await DotNet.invokeMethodAsync('SubmitPaypalNonce', objReference, payload.nonce);
        },
    }, '#paypal-buttons');
}