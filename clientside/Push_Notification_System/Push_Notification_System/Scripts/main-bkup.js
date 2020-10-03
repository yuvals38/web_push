

//'use strict';

//const applicationServerPublicKey = 'BHaqkRM6AgASvuEXMPLlkn4bo2fchUkzFC8eUPwEUfa8xqOLj5d6NGqUp-HU9aANj6Lmw3XJDebN5OGp848gUEE';

//const pushButton = document.querySelector('.js-push-btn');

//const testpushButton = document.querySelector('.js-testpush-btn');

//const uri = 'http://localhost:60425/api/products';

//let isSubscribed = false;
//let swRegistration = null;


//function initializeUI() {

//    swRegistration.pushManager.getSubscription().then(
//        testpushButton.addEventListener('click', function () {
//            const title = 'Push Title';
//            const options = {
//                body: 'Testing notification.\nSecond line of body text :)'
//            };
//            swRegistration.showNotification(title, options);
//        }));

//    pushButton.addEventListener('click', function () {
//        pushButton.disabled = true;
//        testPushBtn.disabled = false;
//        if (isSubscribed) {
//            unsubscribeUser();
//        } else {
//            subscribeUser();
//        }
//    });

//    function unsubscribeUser() {
//        swRegistration.pushManager.getSubscription()
//            .then(function (subscription) {
//                if (subscription) {
//                    //application server deletes subscription
//                    return subscription.unsubscribe();
//                }
//            })
//            .catch(function (error) {
//                console.log('Error unsubscribing', error);
//            })
//            .then(function () {
//                updateSubscriptionOnServer(null);

//                console.log('User is unsubscribed.');
//                isSubscribed = false;

//                updateBtn();
//            });
//    }

//    // Set the initial subscription value
//    swRegistration.pushManager.getSubscription()
//        .then(function (subscription) {
//            isSubscribed = !(subscription === null);

//            updateSubscriptionOnServer(subscription);

//            if (isSubscribed) {
//                console.log('User IS subscribed.');
//            } else {
//                console.log('User is NOT subscribed.');
//            }

//            updateBtn();
//        });
//}

//function subscribeUser() {
//    const applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);
//    swRegistration.pushManager.subscribe({
//        userVisibleOnly: true,
//        applicationServerKey: applicationServerKey
//    })
//        .then(function (subscription) {
//            console.log('User is subscribed.');

//            updateSubscriptionOnServer(subscription);

//            isSubscribed = true;

//            updateBtn();
//        })
//        .catch(function (err) {
//            console.log('Failed to subscribe the user: ', err);
//            updateBtn();
//        });
//}

//function updateSubscriptionOnServer(subscription) {

//    const subscriptionJson = document.querySelector('.js-subscription-json');
//    const subscriptionDetails =
//        document.querySelector('.js-subscription-details');

//    if (subscription) {

//        subscriptionJson.textContent = JSON.stringify(subscription);
//        document.getElementById('SubscriptionData').value = subscriptionJson.textContent;
//        subscriptionDetails.classList.remove('is-invisible');
//        console.log(subscriptionJson.textContent);

//        // TODO: Send subscription to application server
//        sendRequest(subscriptionJson.textContent);
//        //
//    } else {
//        subscriptionDetails.classList.add('is-invisible');
//    }
//}

//function sendRequest(subscriptioninfo) {
   
//    var Test = $('#prodId').val();
//    $.ajax({
//        type: "POST",
//        url: uri + '/',
//        data: { '': JSON.stringify(subscriptioninfo) }
//    }).done(function (data) {
//        alert(data);
//    }).fail(function (jqXHR, textStatus, errorThrown) {
//        alert(jqXHR.responseText || textStatus);
//    });
//}

//function updateBtn() {
//    if (Notification.permission === 'denied') {
//        pushButton.textContent = 'Push Messaging Blocked.';
//        pushButton.disabled = true;

//        testPushBtn.disabled = true;

//        updateSubscriptionOnServer(null);
//        return;
//    }
//    if (isSubscribed) {
//        pushButton.textContent = 'Disable Push Messaging';
//        testPushBtn.textContent = 'Push testing disabled';
//    } else {
//        pushButton.textContent = 'Enable Push Messaging';
//        testPushBtn.textContent = 'Push testing enabled';
//    }

//    pushButton.disabled = false;
//    testPushBtn.disabled = false;
//}
//navigator.serviceWorker.register('./sw.js')
//    .then(function (swReg) {
//        console.log('Service Worker is registered', swReg);

//        swRegistration = swReg;
//        initializeUI();
//    })


//function testPushBtn() {
//    notifyButton.addEventListener('click', function () {
//        const title = 'Simple Title';
//        const options = {
//            body: 'Simple piece of body text.\nSecond line of body text :)'
//        };
//        swRegistration.showNotification(title, options);
//    });
//    //navigator.serviceWorker.controller.push();
//    console.log('button pushed');
//}

//function urlB64ToUint8Array(base64String) {
//    const padding = '='.repeat((4 - base64String.length % 4) % 4);
//    const base64 = (base64String + padding)
//        .replace(/\-/g, '+')
//        .replace(/_/g, '/');

//    const rawData = window.atob(base64);
//    const outputArray = new Uint8Array(rawData.length);

//    for (let i = 0; i < rawData.length; ++i) {
//        outputArray[i] = rawData.charCodeAt(i);
//    }
//    return outputArray;
//}

//if ('serviceWorker' in navigator && 'PushManager' in window) {
//    console.log('Service Worker and Push is supported');

//    navigator.serviceWorker.register('./sw.js')
//        .then(function (swReg) {
//            console.log('Service Worker is registered', swReg);

//            swRegistration = swReg;
//        })
//        .catch(function (error) {
//            console.error('Service Worker Error', error);
//        });
//} else {
//    console.warn('Push messaging is not supported');
//    pushButton.textContent = 'Push Not Supported';
//}
