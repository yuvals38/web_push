'use strict';
self.addEventListener('push', function (event) {
    console.log('[Service Worker] Push Received.');
    console.log(`[Service Worker] Push had this data: "${event.data.text()}"`);

    var data = {};
    if (event.data) {
        data = event.data.json();
     }
    
    console.log(data.title);

    const title = data.title;
    const options = {
        body: data.body,
        icon: 'images/push-icon.jpg',
        badge: 'images/push-icon.jpg'
    };

    event.waitUntil(self.registration.showNotification(title, options));
});

//self.addEventListener('notificationclick', function (event) {
self.onnotificationclick = function (event) {
    console.log('[Service Worker] Notification click Received.');

    event.notification.close();

    event.waitUntil(
        clients.openWindow('https://developers.google.com/web/')
    );
};
