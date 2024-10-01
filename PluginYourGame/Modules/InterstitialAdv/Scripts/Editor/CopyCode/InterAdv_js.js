
let nowFullAdOpen = false;

function InterAdvShow() {
    try {
        if (nowFullAdOpen !== true && ysdk !== null) {
            ysdk.adv.showFullscreenAdv(
                {
                    callbacks: {
                        onOpen: () => {
                            LogStyledMessage('Open Interstitial Adv');
                            nowFullAdOpen = true;
                            if (initGame === true) {
                                YG2Instance('OpenInterAdv');
                            }
                        },
                        onClose: (wasShown) => {
                            nowFullAdOpen = false;
                            if (initGame === true) {
                                if (wasShown) {
                                    YG2Instance('CloseInterAdv', 'true');
                                }
                                else {
                                    YG2Instance('CloseInterAdv', 'false');
                                }
                            }
                            FocusGame();
                        },
                        onError: (error) => {
                            console.error('Error Interstitial Adv', error);
                            YG2Instance('ErrorInterAdv');
                            FocusGame();
                        }
                    }
                });
        }
    }
    catch (e) {
        console.error('CRASH Interstitial Adv Show: ', e.message);
    }
}