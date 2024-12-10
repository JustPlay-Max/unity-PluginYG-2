var playerData = NO_DATA;
let player = null;

function InitPlayer() {
    return new Promise((resolve) => {
        try {
            if (ysdk == null) {
                Final(NotAuthorized());
            }
            else {
                let _scopes = ___scopes___;
                ysdk.getPlayer({ scopes: _scopes })
                    .then(_player => {
                        player = _player;

                        let playerName = player.getName();
                        let playerPhoto = player.getPhoto('___photoSize___');

                        if (!_scopes) {
                            playerName = "anonymous";
                            playerPhoto = "NO_DATA";
                        }

                        if (player.getMode() === 'lite') {
                            LogStyledMessage('Not Authorized');
                            Final(NotAuthorized());
                        }
                        else {
                            let authJson = {
                                "playerAuth": "resolved",
                                "playerName": playerName,
                                "playerId": player.getUniqueID(),
                                "playerPhoto": playerPhoto,
                                "payingStatus": player.getPayingStatus()
                            };
                            Final(JSON.stringify(authJson));
                        }
                    }).catch(e => {
                        console.error('Authorized err: ', e.message);
                        Final(NotAuthorized());
                    });
            }
        }
        catch (e) {
            console.error('CRASH init Player: ', e.message);
            Final(NotAuthorized());
        }

        function Final(res) {
            playerData = res;
            YG2Instance('SetAuth', res);
            resolve(res);
        }
    });
}

function NotAuthorized() {
    let authJson = {
        "playerAuth": "rejected",
        "playerName": "unauthorized",
        "playerId": "unauthorized",
        "playerPhoto": "null",
        "payingStatus": "unknown"
    };
    return JSON.stringify(authJson);
}

function OpenAuthDialog() {
    if (ysdk !== null) {
        try {
            ysdk.auth.openAuthDialog().then(() => {
                InitPlayer(true)
                    .then(() => {
                        YG2Instance('GetDataInvoke');
                    });
            });
        }
        catch (e) {
            LogStyledMessage('CRASH Open Auth Dialog: ', e.message);
        }
    }
}