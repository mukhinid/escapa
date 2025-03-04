mergeInto(LibraryManager.library, {

  Init: function(callback) {
    YaGames.init().then(_ysdk => {
      window.ysdk = _ysdk;

      window.ysdk.features.LoadingAPI.ready();

      window.ysdk.getPlayer().then(_player => {
        window.player = _player;
      }).catch(console.error);

      {{{ makeDynCall('v', 'callback') }}}(0);
    });
  },

  GetEnvironmentJson: function() {
    const str = JSON.stringify(window.ysdk.environment);
    const bufferSize = lengthBytesUTF8(str) + 1;
    const buffer = _malloc(bufferSize);
    stringToUTF8(str, buffer, bufferSize);
    return buffer;
  },

  ShowFullScreenAdv: function(callback) {
    window.ysdk.adv.showFullscreenAdv({
      callbacks: {
        onOpen: function() {
          {{{ makeDynCall('vi', 'callback') }}}(0);
        },
        onClose: function(wasShown) {
          {{{ makeDynCall('vi', 'callback') }}}(wasShown ? 1 : 2);
        },
        onError: function(error) {
          {{{ makeDynCall('vi', 'callback') }}}(3);
        },
        onOffline: function() {
          {{{ makeDynCall('vi', 'callback') }}}(4);
        },
      },
  })},

  IsAuthorized: function() {
    if (window.player) {
      return window.player.getMode() !== 'lite';
    }
    return false;
  },

  SendScore: function(lbName, score) {
    window.ysdk.getLeaderboards().then(lb => {
      const name = UTF8ToString(lbName);
      lb.setLeaderboardScore(name, score);
    });
  },

  Authorize: function(callback) {
    window.ysdk.auth.openAuthDialog().then(() => {
      window.ysdk.getPlayer().then(_player => {
        window.player = _player;
        {{{ makeDynCall('vi', 'callback') }}}(1);
      }).catch(err => {
        console.error(err);
        {{{ makeDynCall('vi', 'callback') }}}(0);
      });
    }).catch(() => {
      {{{ makeDynCall('vi', 'callback') }}}(0);
    });
  },

});
