require({
    paths: {
        'scribe': './scribe-editor/scribe/scribe',
        'scribe-plugin-blockquote-command': './scribe-editor/scribe-plugins/scribe-plugin-blockquote-command',
        'scribe-plugin-code-command': './scribe-editor/scribe-plugins/scribe-plugin-code-command',
        'scribe-plugin-curly-quotes': './scribe-editor/scribe-plugins/scribe-plugin-curly-quotes',
        'scribe-plugin-formatter-plain-text-convert-new-lines-to-html': './scribe-editor/scribe-plugins/scribe-plugin-formatter-plain-text-convert-new-lines-to-html',
        'scribe-plugin-heading-command': './scribe-editor/scribe-plugins/scribe-plugin-heading-command',
        'scribe-plugin-intelligent-unlink-command': './scribe-editor/scribe-plugins/scribe-plugin-intelligent-unlink-command',
        'scribe-plugin-keyboard-shortcuts': './scribe-editor/scribe-plugins/scribe-plugin-keyboard-shortcuts',
        'scribe-plugin-link-prompt-command': './scribe-editor/scribe-plugins/scribe-plugin-link-prompt-command',
        'scribe-plugin-sanitizer': './scribe-editor/scribe-plugins/scribe-plugin-sanitizer',
        'scribe-plugin-smart-lists': './scribe-editor/scribe-plugins/scribe-plugin-smart-lists',
        'scribe-plugin-toolbar': './scribe-editor/scribe-plugins/scribe-plugin-toolbar',
        'scribe-plugin-content-cleaner': './scribe-editor/scribe-plugins/scribe-plugin-content-cleaner',
        'scribe-plugin-image-prompt-command': './scribe-editor/scribe-plugins/scribe-plugin-image-prompt-command'
    }
}, [
        'scribe',
        'scribe-plugin-blockquote-command',
        'scribe-plugin-code-command',
        'scribe-plugin-curly-quotes',
        'scribe-plugin-formatter-plain-text-convert-new-lines-to-html',
        'scribe-plugin-heading-command',
        'scribe-plugin-intelligent-unlink-command',
        'scribe-plugin-keyboard-shortcuts',
        'scribe-plugin-link-prompt-command',
        'scribe-plugin-sanitizer',
        'scribe-plugin-smart-lists',
        'scribe-plugin-toolbar',
        'scribe-plugin-content-cleaner',
        'scribe-plugin-image-prompt-command'
    ], function (
        Scribe,
        scribePluginBlockquoteCommand,
        scribePluginCodeCommand,
        scribePluginCurlyQuotes,
        scribePluginFormatterPlainTextConvertNewLinesToHtml,
        scribePluginHeadingCommand,
        scribePluginIntelligentUnlinkCommand,
        scribePluginKeyboardShortcuts,
        scribePluginLinkPromptCommand,
        scribePluginSanitizer,
        scribePluginSmartLists,
        scribePluginToolbar,
        scribePluginContentCleaner,
        scribePluginImagePromptCommand
    )
    {

        'use strict';

        var scribe = new Scribe(document.querySelector('.scribe'), { allowBlockElements: true });

        scribe.on('content-changed', updateHTML);

        function updateHTML()
        {
            document.querySelector('.scribe-html').value = scribe.getHTML();
        }

        /**
         * Keyboard shortcuts
         */

        var ctrlKey = function (event) { return event.metaKey || event.ctrlKey; };

        var commandsToKeyboardShortcutsMap = Object.freeze({
            bold: function (event) { return event.metaKey && event.keyCode === 66; }, // b
            italic: function (event) { return event.metaKey && event.keyCode === 73; }, // i
            strikeThrough: function (event) { return event.altKey && event.shiftKey && event.keyCode === 83; }, // s
            removeFormat: function (event) { return event.altKey && event.shiftKey && event.keyCode === 65; }, // a
            linkPrompt: function (event) { return event.metaKey && !event.shiftKey && event.keyCode === 75; }, // k
            unlink: function (event) { return event.metaKey && event.shiftKey && event.keyCode === 75; }, // k,
            insertUnorderedList: function (event) { return event.altKey && event.shiftKey && event.keyCode === 66; }, // b
            insertOrderedList: function (event) { return event.altKey && event.shiftKey && event.keyCode === 78; }, // n
            blockquote: function (event) { return event.altKey && event.shiftKey && event.keyCode === 87; }, // w
            code: function (event) { return event.metaKey && event.shiftKey && event.keyCode === 76; }, // l
            h2: function (event) { return ctrlKey(event) && event.keyCode === 50; } // 2
        });

        /**
         * Example validation functions
         */
        function linkValidator(url)
        {
            if (/dailymail/.test(url))
            {
                return {
                    valid: false,
                    message: "I'm afraid I can't let you link to that Dave..."
                }
            }

            return {
                valid: true
            };
        }

        /**
         * Plugins
         */

        scribe.use(scribePluginBlockquoteCommand());
        scribe.use(scribePluginCodeCommand());
        scribe.use(scribePluginHeadingCommand(1));
        scribe.use(scribePluginHeadingCommand(2));
        scribe.use(scribePluginIntelligentUnlinkCommand());
        scribe.use(scribePluginLinkPromptCommand({
            validation: linkValidator,
            transforms: {
                post: [function (link)
                {
                    return link.replace('www.guardian.co.uk', 'www.theguardian.com');
                }]
            }
        }));
        scribe.use(scribePluginToolbar(document.querySelector('.toolbar')));
        scribe.use(scribePluginSmartLists());
        scribe.use(scribePluginCurlyQuotes());
        scribe.use(scribePluginKeyboardShortcuts(commandsToKeyboardShortcutsMap));
        scribe.use(scribePluginContentCleaner());
        scribe.use(scribePluginImagePromptCommand());

        /*
        // WARNING: Formatters will remove any markup-tag not specified here (including attributes)
        // Formatters
        scribe.use(scribePluginSanitizer({
            tags: {
                p: {},
                br: {},
                b: {},
                strong: {},
                i: {},
                strike: {},
                blockquote: {},
                code: {},
                ol: {},
                ul: {},
                li: {},
                a: { href: true },
                img: { src: true },
                h1: {},
                h2: {},
                u: {}
            }
        }));
        */

        scribe.use(scribePluginFormatterPlainTextConvertNewLinesToHtml());

        if (scribe.allowsBlockElements())
        {
            scribe.setContent('<p>Hello, World!</p>');
        } else
        {
            scribe.setContent('Hello, World!');
        }
    });
