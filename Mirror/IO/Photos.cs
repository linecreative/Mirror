﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;


namespace Mirror.IO
{
    static class Photos
    {
        const string Photo = "temporary";
        const string Extension = ".jpg";
        const string PhotoName = Photo + Extension;

        internal static Task<StorageFile> CreateAsync() =>
            KnownFolders.PicturesLibrary
                        .CreateFileAsync(PhotoName,
                                         CreationCollisionOption.GenerateUniqueName)
                        .AsTask();

        internal static async Task CleanupAsync()
        {
            var files = await KnownFolders.PicturesLibrary.GetFilesAsync();

            var deletions =
                files.Where(file => file.DisplayName.Contains(Photo))
                     .Select(file => file.DeleteAsync().AsTask());

            await Task.WhenAll(deletions);
        }
    }
}