# Background

Create plugin for Microsoft Edge that disables peoples ability to prevent copy+paste
into forms. This exists already for Firefox and Google Chrome.

https://twitter.com/onovotny/status/1025861667551363073

(howdy oren!)

#  Backlog

- [x] Pull down the source code for dfwp
- [x] Ensure tests/*.html are functional and working
- [x] What chrome specific API's are used?

- [ ] How are we going to migrate?
- [ ] Migrate DFWP.storage which is aliased to chrome.storage.sync
- [ ] Migrate 

- [ ] What's required to publish an addin to edge?
- [ ] How to enable developer mode and get REPL setup?
- [ ] Get it working for tests/*
- [ ] Get regex match working.
- [ ] Port chrome specific API's over to Edge
- [ ] Publish addin to the store
- [ ] Drink beer.

# Learnings

dfwp contains a singleton `DFWP`

DFWP.RuleView
DFWP.Rules
DFWP.storage (aliased to chrome.storage.sync in dfwp.js)

## Chrome Specific APIs

### chrome.tabs

The `chrome.tabs` API is used to create, modify, and rearrange tabs in the browser. Most methods and events can be used without declaring any permissions in the extension's manifest file. However, access to the url, title, or favIconUrl properties of tabs.Tab is requiredthen the `tabs` permission in the manifest must be declared.
https://developer.chrome.com/extensions/tabs


## internals

In background.js DFWP subscribes to three event handlers

`chrome.runtime.onMessage.addListener`
`chrome.storage.onChanged.addListener`
`chrome.tabs.onActivated.addListener`

When any of these event handlers `OnNext` the method `checkIfActive` is triggered. 

chrome.tabs.get


## Google Chrome

## Microsoft Edge

To enable developer mode follow [these instructions](https://docs.microsoft.com/en-us/microsoft-edge/extensions/guides/adding-and-removing-extensions). Specifically check the "enable extension developer features". Then you will need to restart Microsoft Edge. Once it has loaded back up mash the `...` and head to "Load Extensions" menu item.

## dfwp internals

Chrome specifc APIs

content.js

chrome.runtime.onMessage.addListener(({ active }) => {
  if (active) {
    document.addEventListener('copy', forceBrowserDefault, true);
    document.addEventListener('cut', forceBrowserDefault, true);
    document.addEventListener('paste', forceBrowserDefault, true);
  } else {
    document.removeEventListener('copy', forceBrowserDefault, true);
    document.removeEventListener('cut', forceBrowserDefault, true);
    document.removeEventListener('paste', forceBrowserDefault, true);
  }
});

chrome.runtime.sendMessage({ didLoad: true });

dfwp.js:

  if (chrome.storage.sync) {
    DFWP.storage = chrome.storage.sync;
  } else {
    DFWP.storage = chrome.storage.local;
  }


Singleton of DFWP.

- popup.html
- options.html
- dfwp.js - the meat?


# Retrospective
