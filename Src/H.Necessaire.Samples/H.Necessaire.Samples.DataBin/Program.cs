using System;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.DataBin
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DataBinMeta attachmentMeta = new()
            {
                Name = "attachment.png",
                Format = new DataBinFormatInfo
                {
                    ID = "ImagePng",
                    Extension = "png",
                    MimeType = "image/png",
                    Encoding = null,
                },
            };

            H.Necessaire.DataBin attachment
                = attachmentMeta
                .ToBin(
                    x => x.Name.OpenEmbeddedResource().ToDataBinStream().AsTask()
                );

            using (ImADataBinStream data = await attachment.OpenDataBinStream())
            {
                Console.WriteLine(await data.DataStream.ReadAsStringAsync());
            }
        }
    }
}
