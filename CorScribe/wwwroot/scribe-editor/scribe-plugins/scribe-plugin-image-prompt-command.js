define(function ()
{

    /**
     * Adds a command for using CODEs.
     */

    'use strict';

    return function ()
    {

        function addAttributes(html, attrs)
        {
            var host = document.createElement('div');
            host.innerHTML = unescape(html);

            var frame = host.children[0];
            for (var prop in attrs)
            {
                frame.setAttribute(prop, attrs[prop]);
            }

            return host.innerHTML;
        }



        return function (scribe)
        {
            debugger;
            var imagePromptCommand = new scribe.api.Command('insertImage');
            imagePromptCommand.nodeName = 'IMG';

            if (typeof options == 'function')
            {
                prompt = options;
            };


            imagePromptCommand.execute = function ()
            {
                alert("foo");

                scribe.transactionManager.run(function ()
                {
                    //// TODO: When this command supports all types of ranges we can abstract
                    //// it and use it for any command that applies inline styles.
                    //var selection = new scribe.api.Selection();
                    //var range = selection.range;

                    //var selectedHtmlDocumentFragment = range.extractContents();

                    //var codeElement = document.createElement('code');
                    //codeElement.appendChild(selectedHtmlDocumentFragment);

                    //range.insertNode(codeElement);

                    //range.selectNode(codeElement);

                    //// Re-apply the range
                    //selection.selection.removeAllRanges();
                    //selection.selection.addRange(range);

                    console.log("foo");

                    /*
                    debugger;
                    console.log("foo");
                    link = prompt ? prompt() : window.prompt('Enter image url');
                    if (!link) return false;
                    if (typeof link === 'object')
                    {
                        throw Error("imagePromptCommand.execute: extend not defined");
                        // If some extra properties were passed from prompt
                        options = extend(options, link);
                    } else if (typeof link === 'string')
                    {
                        options.url = link
                    };

                    if (!/^https?\:\/\//.test(options.url))
                    {
                        options.url = location.protocol + '//' + options.url;
                    };

                    var url = options.url;
                    var html = addAttributes('<img src=' + url + '>', options.attributes);
                    */
                });
            };


            // There is no native command for CODE elements, so we have to provide
            // our own `queryState` method.
            // TODO: Find a way to make it explicit what the sequence of commands will
            // be.
            // if the command has been executed
            imagePromptCommand.queryState = function ()
            {
                var selection = new scribe.api.Selection();
                return !!selection.getContaining(function (node)
                {
                    return node.nodeName === this.nodeName;
                }.bind(this));
            };


            // There is no native command for CODE elements, so we have to provide
            // our own `queryEnabled` method.
            // TODO: Find a way to make it explicit what the sequence of commands will
            // be.
            // if the command can be executed with the current selection
            imagePromptCommand.queryEnabled = function ()
            {
                var selection = new scribe.api.Selection();
                var range = selection.range;

                // TODO: Support uncollapsed ranges
                return !range.collapsed;
            };


            /*
            imagePromptCommand.execute = function ()
            {
                debugger;
                console.log("foo");
                link = prompt ? prompt() : window.prompt('Enter image url');
                if (!link) return false;
                if (typeof link === 'object')
                {
                    throw Error("imagePromptCommand.execute: extend not defined");
                    // If some extra properties were passed from prompt
                    options = extend(options, link);
                } else if (typeof link === 'string')
                {
                    options.url = link
                };

                if (!/^https?\:\/\//.test(options.url))
                {
                    options.url = location.protocol + '//' + options.url;
                };

                var url = options.url;
                var html = addAttributes('<img src=' + url + '>', options.attributes);

                scribe.api.SimpleCommand.prototype.execute.call(this, html);
            }
            */

            scribe.commands.imagePrompt = imagePromptCommand;
        };
    };

});
