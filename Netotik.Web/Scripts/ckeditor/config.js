/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    config.skin = 'moono';
    config.filebrowserImageUploadUrl = '/admin/editor/upload';
    CKEDITOR.config.protectedSource.push(/<(script)[^>]*>.*<\/script>/ig);
    CKEDITOR.config.extraAllowedContent = 'script(*)[*]{*};';
};

