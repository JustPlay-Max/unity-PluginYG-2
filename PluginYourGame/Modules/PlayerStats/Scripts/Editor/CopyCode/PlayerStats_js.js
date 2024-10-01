
var statsSaves = NO_DATA;

function SetState(key, value) {
    if (player == null) {
        console.error('SetStats: Player is not defined');
        return;
    }
    try {
        var increments = {};
        increments[key] = value;
        player.incrementStats(increments);
    } catch (e) {
        console.error('CRASH Set Stats: ', e.message);
    }
}

function GetStats() {
    return new Promise((resolve) => {
        if (ysdk == null) {
            Final(NO_DATA);
            return;
        }
        try {
            player.getStats()
                .then(stats => {
                    Final(JSON.stringify(stats));
                }).catch(e => {
                    console.error('Get Stats Error!', e.message);
                    Final(NO_DATA);
                });
        }
        catch (e) {
            console.error('CRASH Get Stats Cloud: ', e.message);
            Final(NO_DATA);
        }

        function Final(res) {
            statsSaves = res;
            YG2Instance('ReceiveStats', res);
            resolve(res);
        }
    });
}

function SetAllStats(jsonStats) {
    if (ysdk == null || player == null) {
        console.error('SetAllStats: Error initialization');
        return;
    }
    try {
        player.setStats(JSON.parse(jsonStats));
    } catch (e) {
        console.error('CRASH Set All Stats: ', e.message);
    }
}