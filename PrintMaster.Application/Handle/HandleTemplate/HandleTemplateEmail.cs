using PrintMaster.Domain.Entities;

namespace PrintMaster.Application.Handle.HandleTemplate
{
    public class HandleTemplateEmail
    {
        public static string GenerateNotificationBillEmail(Bill bill)
        {
            string htmlContent = $@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html
  dir=""ltr""
  xmlns=""http://www.w3.org/1999/xhtml""
  xmlns:o=""urn:schemas-microsoft-com:office:office""
  lang=""vi""
  style=""padding: 0; margin: 0""
>
  <head>
    <meta charset=""UTF-8"" />
    <meta content=""width=device-width, initial-scale=1"" name=""viewport"" />
    <meta name=""x-apple-disable-message-reformatting"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
    <meta content=""telephone=no"" name=""format-detection"" />
    <title>New Template 4</title>
    <!--[if (mso 16)
      ]><style type=""text/css"">
        a {{
          text-decoration: none;
        }}
      </style><!
    [endif]-->
    <!--[if gte mso 9
      ]><style>
        sup {{
          font-size: 100% !important;
        }}
      </style><!
    [endif]-->
    <!--[if gte mso 9
      ]><xml>
        <o:OfficeDocumentSettings>
          <o:AllowPNG></o:AllowPNG> <o:PixelsPerInch>96</o:PixelsPerInch>
        </o:OfficeDocumentSettings>
      </xml>
    <![endif]-->
    <style type=""text/css"">
      #outlook a {{
        padding: 0;
      }}
      .ExternalClass {{
        width: 100%;
      }}
      .ExternalClass,
      .ExternalClass p,
      .ExternalClass span,
      .ExternalClass font,
      .ExternalClass td,
      .ExternalClass div {{
        line-height: 100%;
      }}
      .es-button {{
        mso-style-priority: 100 !important;
        text-decoration: none !important;
      }}
      a[x-apple-data-detectors] {{
        color: inherit !important;
        text-decoration: none !important;
        font-size: inherit !important;
        font-family: inherit !important;
        font-weight: inherit !important;
        line-height: inherit !important;
      }}
      .es-desk-hidden {{
        display: none;
        float: left;
        overflow: hidden;
        width: 0;
        max-height: 0;
        line-height: 0;
        mso-hide: all;
      }}
      @media only screen and (max-width: 600px) {{
        p,
        ul li,
        ol li,
        a {{
          line-height: 150% !important;
        }}
        h1,
        h2,
        h3,
        h1 a,
        h2 a,
        h3 a {{
          line-height: 120% !important;
        }}
        h1 {{
          font-size: 30px !important;
          text-align: center;
        }}
        h2 {{
          font-size: 26px !important;
          text-align: center;
        }}
        h3 {{
          font-size: 20px !important;
          text-align: center;
        }}
        .es-header-body h1 a,
        .es-content-body h1 a,
        .es-footer-body h1 a {{
          font-size: 30px !important;
        }}
        .es-header-body h2 a,
        .es-content-body h2 a,
        .es-footer-body h2 a {{
          font-size: 26px !important;
        }}
        .es-header-body h3 a,
        .es-content-body h3 a,
        .es-footer-body h3 a {{
          font-size: 20px !important;
        }}
        .es-menu td a {{
          font-size: 14px !important;
        }}
        .es-header-body p,
        .es-header-body ul li,
        .es-header-body ol li,
        .es-header-body a {{
          font-size: 16px !important;
        }}
        .es-content-body p,
        .es-content-body ul li,
        .es-content-body ol li,
        .es-content-body a {{
          font-size: 16px !important;
        }}
        .es-footer-body p,
        .es-footer-body ul li,
        .es-footer-body ol li,
        .es-footer-body a {{
          font-size: 13px !important;
        }}
        .es-infoblock p,
        .es-infoblock ul li,
        .es-infoblock ol li,
        .es-infoblock a {{
          font-size: 12px !important;
        }}
        *[class=""gmail-fix""] {{
          display: none !important;
        }}
        .es-m-txt-c,
        .es-m-txt-c h1,
        .es-m-txt-c h2,
        .es-m-txt-c h3 {{
          text-align: center !important;
        }}
        .es-m-txt-r,
        .es-m-txt-r h1,
        .es-m-txt-r h2,
        .es-m-txt-r h3 {{
          text-align: right !important;
        }}
        .es-m-txt-l,
        .es-m-txt-l h1,
        .es-m-txt-l h2,
        .es-m-txt-l h3 {{
          text-align: left !important;
        }}
        .es-m-txt-r img,
        .es-m-txt-c img,
        .es-m-txt-l img {{
          display: inline !important;
        }}
        .es-button-border {{
          display: block !important;
        }}
        a.es-button,
        button.es-button {{
          font-size: 20px !important;
          display: block !important;
          padding: 10px 0px 10px 0px !important;
        }}
        .es-btn-fw {{
          border-width: 10px 0px !important;
          text-align: center !important;
        }}
        .es-adaptive table,
        .es-btn-fw,
        .es-btn-fw-brdr,
        .es-left,
        .es-right {{
          width: 100% !important;
        }}
        .es-content table,
        .es-header table,
        .es-footer table,
        .es-content,
        .es-footer,
        .es-header {{
          width: 100% !important;
          max-width: 600px !important;
        }}
        .es-adapt-td {{
          display: block !important;
          width: 100% !important;
        }}
        .adapt-img {{
          width: 100% !important;
          height: auto !important;
        }}
        .es-m-p0 {{
          padding: 0px !important;
        }}
        .es-m-p0r {{
          padding-right: 0px !important;
        }}
        .es-m-p0l {{
          padding-left: 0px !important;
        }}
        .es-m-p0t {{
          padding-top: 0px !important;
        }}
        .es-m-p0b {{
          padding-bottom: 0 !important;
        }}
        .es-m-p20b {{
          padding-bottom: 20px !important;
        }}
        .es-mobile-hidden,
        .es-hidden {{
          display: none !important;
        }}
        tr.es-desk-hidden,
        td.es-desk-hidden,
        table.es-desk-hidden {{
          width: auto !important;
          overflow: visible !important;
          float: none !important;
          max-height: inherit !important;
          line-height: inherit !important;
        }}
        tr.es-desk-hidden {{
          display: table-row !important;
        }}
        table.es-desk-hidden {{
          display: table !important;
        }}
        td.es-desk-menu-hidden {{
          display: table-cell !important;
        }}
        .es-menu td {{
          width: 1% !important;
        }}
        table.es-table-not-adapt,
        .esd-block-html table {{
          width: auto !important;
        }}
        table.es-social {{
          display: inline-block !important;
        }}
        table.es-social td {{
          display: inline-block !important;
        }}
        .es-desk-hidden {{
          display: table-row !important;
          width: auto !important;
          overflow: visible !important;
          max-height: inherit !important;
        }}
      }}
      @media screen and (max-width: 384px) {{
        .mail-message-content {{
          width: 414px !important;
        }}
      }}
    </style>
  </head>
  <body
    data-new-gr-c-s-loaded=""14.1175.0""
    style=""
      width: 100%;
      font-family: arial, 'helvetica neue', helvetica, sans-serif;
      -webkit-text-size-adjust: 100%;
      -ms-text-size-adjust: 100%;
      padding: 0;
      margin: 0;
    ""
  >
    <div
      dir=""ltr""
      class=""es-wrapper-color""
      lang=""vi""
      style=""background-color: #333333""
    >
      <!--[if gte mso 9
        ]><v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t"">
          <v:fill type=""tile"" color=""#333333""></v:fill> </v:background
      ><![endif]-->
      <table
        class=""es-wrapper""
        width=""100%""
        cellspacing=""0""
        cellpadding=""0""
        role=""none""
        style=""
          mso-table-lspace: 0pt;
          mso-table-rspace: 0pt;
          border-collapse: collapse;
          border-spacing: 0px;
          padding: 0;
          margin: 0;
          width: 100%;
          height: 100%;
          background-repeat: repeat;
          background-position: center top;
          background-color: #333333;
        ""
      >
        <tr style=""border-collapse: collapse"">
          <td valign=""top"" style=""padding: 0; margin: 0"">
            <table
              cellpadding=""0""
              cellspacing=""0""
              class=""es-content""
              align=""center""
              role=""none""
              style=""
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                border-collapse: collapse;
                border-spacing: 0px;
                table-layout: fixed !important;
                width: 100%;
              ""
            >
              <tr style=""border-collapse: collapse"">
                <td
                  class=""es-adaptive""
                  align=""center""
                  style=""padding: 0; margin: 0""
                >
                  <table
                    class=""es-content-body""
                    style=""
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      border-collapse: collapse;
                      border-spacing: 0px;
                      background-color: transparent;
                      width: 600px;
                    ""
                    cellspacing=""0""
                    cellpadding=""0""
                    align=""center""
                    role=""none""
                  >
                    <tr style=""border-collapse: collapse"">
                      <td
                        style=""
                          padding: 10px;
                          margin: 0;
                          background-position: center top;
                        ""
                        align=""left""
                      >
                        <!--[if mso]><table style=""width:580px""><tr>
    <td style=""width:280px"" valign=""top""><![endif]-->
                        <table
                          class=""es-left""
                          cellspacing=""0""
                          cellpadding=""0""
                          align=""left""
                          role=""none""
                          style=""
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            border-collapse: collapse;
                            border-spacing: 0px;
                            float: left;
                          ""
                        >
                          <tr style=""border-collapse: collapse"">
                            <td
                              align=""left""
                              style=""padding: 0; margin: 0; width: 280px""
                            >
                              <table
                                width=""100%""
                                cellspacing=""0""
                                cellpadding=""0""
                                role=""presentation""
                                style=""
                                  mso-table-lspace: 0pt;
                                  mso-table-rspace: 0pt;
                                  border-collapse: collapse;
                                  border-spacing: 0px;
                                ""
                              >
                                <tr style=""border-collapse: collapse"">
                                  <td
                                    class=""es-infoblock es-m-txt-c""
                                    align=""left""
                                    style=""
                                      padding: 0;
                                      margin: 0;
                                      line-height: 14px;
                                      font-size: 12px;
                                      color: #cccccc;
                                    ""
                                  >
                                    <p
                                      style=""
                                        margin: 0;
                                        -webkit-text-size-adjust: none;
                                        -ms-text-size-adjust: none;
                                        mso-line-height-rule: exactly;
                                        font-family: arial, 'helvetica neue',
                                          helvetica, sans-serif;
                                        line-height: 14px;
                                        color: #cccccc;
                                        font-size: 12px;
                                      ""
                                    >
                                      Put your preheader text here
                                    </p>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                        <!--[if mso]></td><td style=""width:20px""></td><td style=""width:280px"" valign=""top""><![endif]-->
                        <table
                          class=""es-right""
                          cellspacing=""0""
                          cellpadding=""0""
                          align=""right""
                          role=""none""
                          style=""
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            border-collapse: collapse;
                            border-spacing: 0px;
                            float: right;
                          ""
                        >
                          <tr style=""border-collapse: collapse"">
                            <td
                              align=""left""
                              style=""padding: 0; margin: 0; width: 280px""
                            >
                              <table
                                width=""100%""
                                cellspacing=""0""
                                cellpadding=""0""
                                role=""presentation""
                                style=""
                                  mso-table-lspace: 0pt;
                                  mso-table-rspace: 0pt;
                                  border-collapse: collapse;
                                  border-spacing: 0px;
                                ""
                              >
                                <tr style=""border-collapse: collapse"">
                                  <td
                                    align=""right""
                                    class=""es-infoblock es-m-txt-c""
                                    style=""
                                      padding: 0;
                                      margin: 0;
                                      line-height: 14px;
                                      font-size: 12px;
                                      color: #cccccc;
                                    ""
                                  >
                                    <p
                                      style=""
                                        margin: 0;
                                        -webkit-text-size-adjust: none;
                                        -ms-text-size-adjust: none;
                                        mso-line-height-rule: exactly;
                                        font-family: arial, 'helvetica neue',
                                          helvetica, sans-serif;
                                        line-height: 14px;
                                        color: #cccccc;
                                        font-size: 12px;
                                      ""
                                    >
                                      <a
                                        href=""https://viewstripo.email/""
                                        target=""_blank""
                                        class=""view""
                                        style=""
                                          -webkit-text-size-adjust: none;
                                          -ms-text-size-adjust: none;
                                          mso-line-height-rule: exactly;
                                          text-decoration: underline;
                                          color: #cccccc;
                                          font-size: 12px;
                                        ""
                                        >View in browser</a
                                      >
                                    </p>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                        <!--[if mso]></td></tr></table><![endif]-->
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
            <table
              class=""es-content""
              cellspacing=""0""
              cellpadding=""0""
              align=""center""
              role=""none""
              style=""
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                border-collapse: collapse;
                border-spacing: 0px;
                table-layout: fixed !important;
                width: 100%;
              ""
            >
              <tr style=""border-collapse: collapse""></tr>
              <tr style=""border-collapse: collapse"">
                <td align=""center"" style=""padding: 0; margin: 0"">
                  <table
                    class=""es-header-body""
                    style=""
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      border-collapse: collapse;
                      border-spacing: 0px;
                      background-color: #2b2c2c;
                      width: 600px;
                    ""
                    cellspacing=""0""
                    cellpadding=""0""
                    bgcolor=""#2b2c2c""
                    align=""center""
                    role=""none""
                  >
                    <tr style=""border-collapse: collapse"">
                      <td align=""left"" style=""padding: 10px; margin: 0"">
                        <table
                          width=""100%""
                          cellspacing=""0""
                          cellpadding=""0""
                          role=""none""
                          style=""
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            border-collapse: collapse;
                            border-spacing: 0px;
                          ""
                        >
                          <tr style=""border-collapse: collapse"">
                            <td
                              valign=""top""
                              align=""center""
                              style=""padding: 0; margin: 0; width: 580px""
                            >
                              <table
                                width=""100%""
                                cellspacing=""0""
                                cellpadding=""0""
                                role=""presentation""
                                style=""
                                  mso-table-lspace: 0pt;
                                  mso-table-rspace: 0pt;
                                  border-collapse: collapse;
                                  border-spacing: 0px;
                                ""
                              >
                                <tr style=""border-collapse: collapse"">
                                  <td
                                    align=""center""
                                    style=""
                                      padding: 10px;
                                      margin: 0;
                                      font-size: 0;
                                    ""
                                  >
                                    <img
                                      src=""https://eeslewt.stripocdn.email/content/guids/CABINET_48bba02538722a91504d04e69a808999/images/30851517396685967.png""
                                      alt=""InkMastery""
                                      title=""InkMastery""
                                      width=""184""
                                      style=""
                                        display: block;
                                        border: 0;
                                        outline: none;
                                        text-decoration: none;
                                        -ms-interpolation-mode: bicubic;
                                        font-size: 12px;
                                      ""
                                      height=""44""
                                    />
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr style=""border-collapse: collapse"">
                      <td
                        style=""
                          padding: 0;
                          margin: 0;
                          padding-top: 10px;
                          padding-bottom: 10px;
                          background-color: #e2e3e3;
                        ""
                        bgcolor=""#e2e3e3""
                        align=""left""
                      >
                        <table
                          width=""100%""
                          cellspacing=""0""
                          cellpadding=""0""
                          role=""none""
                          style=""
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            border-collapse: collapse;
                            border-spacing: 0px;
                          ""
                        >
                          <tr style=""border-collapse: collapse"">
                            <td
                              valign=""top""
                              align=""center""
                              style=""padding: 0; margin: 0; width: 600px""
                            >
                              <table
                                width=""100%""
                                cellspacing=""0""
                                cellpadding=""0""
                                role=""presentation""
                                style=""
                                  mso-table-lspace: 0pt;
                                  mso-table-rspace: 0pt;
                                  border-collapse: collapse;
                                  border-spacing: 0px;
                                ""
                              >
                                <tr style=""border-collapse: collapse"">
                                  <td style=""padding: 0; margin: 0"">
                                    <table
                                      class=""es-menu""
                                      width=""100%""
                                      cellspacing=""0""
                                      cellpadding=""0""
                                      role=""presentation""
                                      style=""
                                        mso-table-lspace: 0pt;
                                        mso-table-rspace: 0pt;
                                        border-collapse: collapse;
                                        border-spacing: 0px;
                                      ""
                                    >
                                      <tr
                                        class=""links""
                                        style=""border-collapse: collapse""
                                      >
                                        <td
                                          class=""es-hidden""
                                          style=""
                                            margin: 0;
                                            padding-left: 5px;
                                            padding-right: 5px;
                                            padding-top: 0px;
                                            padding-bottom: 0px;
                                            border: 0;
                                          ""
                                          id=""esd-menu-id-4""
                                          esdev-border-color=""#000000""
                                          width=""100%""
                                          bgcolor=""transparent""
                                          align=""center""
                                        >
                                          <a
                                            target=""_blank""
                                            style=""
                                              -webkit-text-size-adjust: none;
                                              -ms-text-size-adjust: none;
                                              mso-line-height-rule: exactly;
                                              text-decoration: none;
                                              display: block;
                                              font-family: arial,
                                                'helvetica neue', helvetica,
                                                sans-serif;
                                              color: #333333;
                                              font-size: 14px;
                                            ""
                                            href=""https://viewstripo.email/""
                                          ></a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
            <table
              class=""es-content""
              cellspacing=""0""
              cellpadding=""0""
              align=""center""
              role=""none""
              style=""
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                border-collapse: collapse;
                border-spacing: 0px;
                table-layout: fixed !important;
                width: 100%;
              ""
            >
              <tr style=""border-collapse: collapse"">
                <td align=""center"" style=""padding: 0; margin: 0"">
                  <table
                    class=""es-content-body""
                    style=""
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      border-collapse: collapse;
                      border-spacing: 0px;
                      background-color: #2b2c2c;
                      width: 600px;
                    ""
                    cellspacing=""0""
                    cellpadding=""0""
                    bgcolor=""#2b2c2c""
                    align=""center""
                    role=""none""
                  >
                    <tr style=""border-collapse: collapse"">
                      <td
                        style=""
                          padding: 0;
                          margin: 0;
                          padding-top: 20px;
                          background-color: #040404;
                        ""
                        bgcolor=""#040404""
                        align=""left""
                      >
                        <table
                          width=""100%""
                          cellspacing=""0""
                          cellpadding=""0""
                          role=""none""
                          style=""
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            border-collapse: collapse;
                            border-spacing: 0px;
                          ""
                        >
                          <tr style=""border-collapse: collapse"">
                            <td
                              valign=""top""
                              align=""center""
                              style=""padding: 0; margin: 0; width: 600px""
                            >
                              <table
                                width=""100%""
                                cellspacing=""0""
                                cellpadding=""0""
                                role=""presentation""
                                style=""
                                  mso-table-lspace: 0pt;
                                  mso-table-rspace: 0pt;
                                  border-collapse: collapse;
                                  border-spacing: 0px;
                                ""
                              >
                                <tr style=""border-collapse: collapse"">
                                  <td
                                    align=""center""
                                    style=""padding: 0; margin: 0; font-size: 0""
                                  >
                                    <a
                                      target=""_blank""
                                      href=""https://viewstripo.email/""
                                      style=""
                                        -webkit-text-size-adjust: none;
                                        -ms-text-size-adjust: none;
                                        mso-line-height-rule: exactly;
                                        text-decoration: underline;
                                        color: #de4a4a;
                                        font-size: 16px;
                                      ""
                                      ><img
                                        class=""adapt-img""
                                        src=""https://eeslewt.stripocdn.email/content/guids/CABINET_ec03bfe212a3efb29764f8962995b8ec/images/97851527778563879.gif""
                                        alt=""InkMastery""
                                        style=""
                                          display: block;
                                          border: 0;
                                          outline: none;
                                          text-decoration: none;
                                          -ms-interpolation-mode: bicubic;
                                        ""
                                        width=""441""
                                        title=""InkMastery""
                                        height=""306""
                                    /></a>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
            <table
              class=""es-content""
              cellspacing=""0""
              cellpadding=""0""
              align=""center""
              role=""none""
              style=""
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                border-collapse: collapse;
                border-spacing: 0px;
                table-layout: fixed !important;
                width: 100%;
              ""
            >
              <tr style=""border-collapse: collapse"">
                <td align=""center"" style=""padding: 0; margin: 0"">
                  <table
                    class=""es-content-body""
                    style=""
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      border-collapse: collapse;
                      border-spacing: 0px;
                      background-color: #2b2c2c;
                      width: 600px;
                    ""
                    cellspacing=""0""
                    cellpadding=""0""
                    bgcolor=""#2b2c2c""
                    align=""center""
                    role=""none""
                  >
                    <tr style=""border-collapse: collapse"">
                      <td
                        style=""
                          margin: 0;
                          padding-left: 20px;
                          padding-right: 20px;
                          padding-top: 25px;
                          padding-bottom: 30px;
                          background-color: #020202;
                        ""
                        bgcolor=""#020202""
                        align=""left""
                      >
                        <table
                          width=""100%""
                          cellspacing=""0""
                          cellpadding=""0""
                          role=""none""
                          style=""
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            border-collapse: collapse;
                            border-spacing: 0px;
                          ""
                        >
                          <tr style=""border-collapse: collapse"">
                            <td
                              valign=""top""
                              align=""center""
                              style=""padding: 0; margin: 0; width: 560px""
                            >
                              <table
                                width=""100%""
                                cellspacing=""0""
                                cellpadding=""0""
                                role=""presentation""
                                style=""
                                  mso-table-lspace: 0pt;
                                  mso-table-rspace: 0pt;
                                  border-collapse: collapse;
                                  border-spacing: 0px;
                                ""
                              >
                                <tr style=""border-collapse: collapse"">
                                  <td
                                    class=""es-m-txt-c""
                                    align=""center""
                                    style=""
                                      margin: 0;
                                      padding-top: 5px;
                                      padding-bottom: 5px;
                                      padding-left: 20px;
                                      padding-right: 20px;
                                    ""
                                  >
                                    <h1
                                      style=""
                                        margin: 0;
                                        line-height: 66px;
                                        mso-line-height-rule: exactly;
                                        font-family: arial, 'helvetica neue',
                                          helvetica, sans-serif;
                                        font-size: 55px;
                                        font-style: normal;
                                        font-weight: normal;
                                        color: #e35367;
                                      ""
                                    >
                                      InkMastery
                                    </h1>
                                  </td>
                                </tr>
                                <tr style=""border-collapse: collapse"">
                                  <td
                                    class=""es-m-txt-c""
                                    align=""center""
                                    style=""
                                      padding: 0;
                                      margin: 0;
                                      padding-left: 20px;
                                      padding-right: 20px;
                                    ""
                                  >
                                    <p
                                      style=""
                                        margin: 0;
                                        -webkit-text-size-adjust: none;
                                        -ms-text-size-adjust: none;
                                        mso-line-height-rule: exactly;
                                        font-family: arial, 'helvetica neue',
                                          helvetica, sans-serif;
                                        line-height: 24px;
                                        color: #cccccc;
                                        font-size: 16px;
                                      ""
                                    >
                                      Cảm ơn quý khách đã tin tưởng và đặt hàng
                                      từ chúng tôi
                                    </p>
                                  </td>
                                </tr>
                                <tr style=""border-collapse: collapse"">
                                  <td
                                    class=""es-m-txt-c""
                                    align=""center""
                                    style=""
                                      padding: 0;
                                      margin: 0;
                                      padding-left: 20px;
                                      padding-right: 20px;
                                    ""
                                  >
                                    <p
                                      style=""
                                        margin: 0;
                                        -webkit-text-size-adjust: none;
                                        -ms-text-size-adjust: none;
                                        mso-line-height-rule: exactly;
                                        font-family: arial, 'helvetica neue',
                                          helvetica, sans-serif;
                                        line-height: 21px;
                                        color: #cccccc;
                                        font-size: 14px;
                                      ""
                                    >
                                      <br />
                                    </p>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
            <table
              cellpadding=""0""
              cellspacing=""0""
              class=""es-footer""
              align=""center""
              role=""none""
              style=""
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                border-collapse: collapse;
                border-spacing: 0px;
                table-layout: fixed !important;
                width: 100%;
                background-color: transparent;
                background-repeat: repeat;
                background-position: center top;
              ""
            >
              <tr style=""border-collapse: collapse"">
                <td align=""center"" style=""padding: 0; margin: 0"">
                  <table
                    class=""es-footer-body""
                    style=""
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      border-collapse: collapse;
                      border-spacing: 0px;
                      background-color: #212121;
                      width: 600px;
                    ""
                    cellspacing=""0""
                    cellpadding=""0""
                    bgcolor=""#212121""
                    align=""center""
                    role=""none""
                  >
                    <tr style=""border-collapse: collapse"">
                      <td
                        align=""left""
                        style=""
                          margin: 0;
                          padding-top: 20px;
                          padding-left: 20px;
                          padding-right: 20px;
                          padding-bottom: 30px;
                        ""
                      >
                        <table
                          width=""100%""
                          cellspacing=""0""
                          cellpadding=""0""
                          role=""none""
                          style=""
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            border-collapse: collapse;
                            border-spacing: 0px;
                          ""
                        >
                          <tr style=""border-collapse: collapse"">
                            <td
                              valign=""top""
                              align=""center""
                              style=""padding: 0; margin: 0; width: 560px""
                            >
                              <table
                                width=""100%""
                                cellspacing=""0""
                                cellpadding=""0""
                                role=""presentation""
                                style=""
                                  mso-table-lspace: 0pt;
                                  mso-table-rspace: 0pt;
                                  border-collapse: collapse;
                                  border-spacing: 0px;
                                ""
                              >
                                <tr style=""border-collapse: collapse"">
                                  <td
                                    align=""center""
                                    style=""
                                      padding: 0;
                                      margin: 0;
                                      padding-bottom: 15px;
                                      font-size: 0;
                                    ""
                                  >
                                    <a
                                      href=""https://viewstripo.email""
                                      target=""_blank""
                                      style=""
                                        -webkit-text-size-adjust: none;
                                        -ms-text-size-adjust: none;
                                        mso-line-height-rule: exactly;
                                        text-decoration: underline;
                                        color: #dbdbdb;
                                        font-size: 14px;
                                      ""
                                      ><img
                                        src=""https://eeslewt.stripocdn.email/content/guids/CABINET_48bba02538722a91504d04e69a808999/images/30851517396685967.png""
                                        alt=""Automotive logo""
                                        title=""Automotive logo""
                                        width=""144""
                                        height=""35""
                                        style=""
                                          display: block;
                                          border: 0;
                                          outline: none;
                                          text-decoration: none;
                                          -ms-interpolation-mode: bicubic;
                                        ""
                                    /></a>
                                  </td>
                                </tr>
                                <tr style=""border-collapse: collapse"">
                                  <td
                                    align=""center""
                                    style=""
                                      padding: 0;
                                      margin: 0;
                                      padding-bottom: 15px;
                                      font-size: 0;
                                    ""
                                  >
                                    <table
                                      class=""es-table-not-adapt es-social""
                                      cellspacing=""0""
                                      cellpadding=""0""
                                      role=""presentation""
                                      style=""
                                        mso-table-lspace: 0pt;
                                        mso-table-rspace: 0pt;
                                        border-collapse: collapse;
                                        border-spacing: 0px;
                                      ""
                                    >
                                      <tr style=""border-collapse: collapse"">
                                        <td
                                          valign=""top""
                                          align=""center""
                                          style=""
                                            padding: 0;
                                            margin: 0;
                                            padding-right: 20px;
                                          ""
                                        >
                                          <a
                                            target=""_blank""
                                            href=""#""
                                            style=""
                                              -webkit-text-size-adjust: none;
                                              -ms-text-size-adjust: none;
                                              mso-line-height-rule: exactly;
                                              text-decoration: underline;
                                              color: #dbdbdb;
                                              font-size: 14px;
                                            ""
                                            ><img
                                              title=""Facebook""
                                              src=""https://eeslewt.stripocdn.email/content/assets/img/social-icons/logo-gray/facebook-logo-gray.png""
                                              alt=""Fb""
                                              width=""32""
                                              height=""32""
                                              style=""
                                                display: block;
                                                border: 0;
                                                outline: none;
                                                text-decoration: none;
                                                -ms-interpolation-mode: bicubic;
                                              ""
                                          /></a>
                                        </td>
                                        <td
                                          valign=""top""
                                          align=""center""
                                          style=""
                                            padding: 0;
                                            margin: 0;
                                            padding-right: 20px;
                                          ""
                                        >
                                          <img
                                            title=""Twitter""
                                            src=""https://eeslewt.stripocdn.email/content/assets/img/social-icons/logo-gray/twitter-logo-gray.png""
                                            alt=""Tw""
                                            width=""32""
                                            height=""32""
                                            style=""
                                              display: block;
                                              border: 0;
                                              outline: none;
                                              text-decoration: none;
                                              -ms-interpolation-mode: bicubic;
                                            ""
                                          />
                                        </td>
                                        <td
                                          valign=""top""
                                          align=""center""
                                          style=""
                                            padding: 0;
                                            margin: 0;
                                            padding-right: 20px;
                                          ""
                                        >
                                          <img
                                            title=""Instagram""
                                            src=""https://eeslewt.stripocdn.email/content/assets/img/social-icons/logo-gray/instagram-logo-gray.png""
                                            alt=""Inst""
                                            width=""32""
                                            height=""32""
                                            style=""
                                              display: block;
                                              border: 0;
                                              outline: none;
                                              text-decoration: none;
                                              -ms-interpolation-mode: bicubic;
                                            ""
                                          />
                                        </td>
                                        <td
                                          valign=""top""
                                          align=""center""
                                          style=""
                                            padding: 0;
                                            margin: 0;
                                            padding-right: 10px;
                                          ""
                                        >
                                          <a
                                            target=""_blank""
                                            href=""#""
                                            style=""
                                              -webkit-text-size-adjust: none;
                                              -ms-text-size-adjust: none;
                                              mso-line-height-rule: exactly;
                                              text-decoration: underline;
                                              color: #dbdbdb;
                                              font-size: 14px;
                                            ""
                                            ><img
                                              title=""Youtube""
                                              src=""https://eeslewt.stripocdn.email/content/assets/img/social-icons/logo-gray/youtube-logo-gray.png""
                                              alt=""Yt""
                                              width=""32""
                                              height=""32""
                                              style=""
                                                display: block;
                                                border: 0;
                                                outline: none;
                                                text-decoration: none;
                                                -ms-interpolation-mode: bicubic;
                                              ""
                                          /></a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                                <tr style=""border-collapse: collapse"">
                                  <td
                                    align=""center""
                                    style=""padding: 0; margin: 0""
                                  >
                                    <p
                                      style=""
                                        margin: 0;
                                        -webkit-text-size-adjust: none;
                                        -ms-text-size-adjust: none;
                                        mso-line-height-rule: exactly;
                                        font-family: arial, 'helvetica neue',
                                          helvetica, sans-serif;
                                        line-height: 20px;
                                        color: #a2a2a2;
                                        font-size: 13px;
                                      ""
                                    >
                                      Automotive Motors, 1230 Deer Creek Road,
                                      Gilbert, CA 99999
                                    </p>
                                    <p
                                      style=""
                                        margin: 0;
                                        -webkit-text-size-adjust: none;
                                        -ms-text-size-adjust: none;
                                        mso-line-height-rule: exactly;
                                        font-family: arial, 'helvetica neue',
                                          helvetica, sans-serif;
                                        line-height: 20px;
                                        color: #a2a2a2;
                                        font-size: 13px;
                                      ""
                                    >
                                      <a
                                        target=""_blank""
                                        style=""
                                          -webkit-text-size-adjust: none;
                                          -ms-text-size-adjust: none;
                                          mso-line-height-rule: exactly;
                                          text-decoration: underline;
                                          color: #dbdbdb;
                                          font-size: 13px;
                                        ""
                                        class=""unsubscribe""
                                        href=""""
                                        >Unsubscribe</a
                                      >
                                      |
                                      <a
                                        target=""_blank""
                                        style=""
                                          -webkit-text-size-adjust: none;
                                          -ms-text-size-adjust: none;
                                          mso-line-height-rule: exactly;
                                          text-decoration: underline;
                                          color: #dbdbdb;
                                          font-size: 13px;
                                        ""
                                        href=""https://viewstripo.email""
                                        >Update Preferences</a
                                      >
                                    </p>
                                  </td>
                                </tr>
                                <tr style=""border-collapse: collapse"">
                                  <td
                                    align=""center""
                                    style=""
                                      padding: 0;
                                      margin: 0;
                                      padding-top: 5px;
                                    ""
                                  >
                                    <p
                                      style=""
                                        margin: 0;
                                        -webkit-text-size-adjust: none;
                                        -ms-text-size-adjust: none;
                                        mso-line-height-rule: exactly;
                                        font-family: arial, 'helvetica neue',
                                          helvetica, sans-serif;
                                        line-height: 20px;
                                        color: #a2a2a2;
                                        font-size: 13px;
                                      ""
                                    >
                                      You are receiving this email because you
                                      have visited our site or asked us about
                                      regular newsletter. Make sure our messages
                                      get to your Inbox (and not your bulk or
                                      junk folders).
                                    </p>
                                    <p
                                      style=""
                                        margin: 0;
                                        -webkit-text-size-adjust: none;
                                        -ms-text-size-adjust: none;
                                        mso-line-height-rule: exactly;
                                        font-family: arial, 'helvetica neue',
                                          helvetica, sans-serif;
                                        line-height: 20px;
                                        color: #a2a2a2;
                                        font-size: 13px;
                                      ""
                                    >
                                      Vectors graphics designed by
                                      <a
                                        target=""_blank""
                                        style=""
                                          -webkit-text-size-adjust: none;
                                          -ms-text-size-adjust: none;
                                          mso-line-height-rule: exactly;
                                          text-decoration: underline;
                                          color: #dbdbdb;
                                          font-size: 13px;
                                        ""
                                        href=""http://www.freepik.com/""
                                        >Freepik</a
                                      >.
                                    </p>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
            <table
              class=""es-footer""
              cellspacing=""0""
              cellpadding=""0""
              align=""center""
              role=""none""
              style=""
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                border-collapse: collapse;
                border-spacing: 0px;
                table-layout: fixed !important;
                width: 100%;
                background-color: transparent;
                background-repeat: repeat;
                background-position: center top;
              ""
            >
              <tr style=""border-collapse: collapse"">
                <td align=""center"" style=""padding: 0; margin: 0"">
                  <table
                    class=""es-footer-body""
                    style=""
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      border-collapse: collapse;
                      border-spacing: 0px;
                      background-color: transparent;
                      width: 600px;
                    ""
                    cellspacing=""0""
                    cellpadding=""0""
                    align=""center""
                    role=""none""
                  >
                    <tr style=""border-collapse: collapse"">
                      <td
                        align=""left""
                        style=""
                          margin: 0;
                          padding-left: 20px;
                          padding-right: 20px;
                          padding-top: 30px;
                          padding-bottom: 30px;
                        ""
                      >
                        <table
                          width=""100%""
                          cellspacing=""0""
                          cellpadding=""0""
                          role=""none""
                          style=""
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            border-collapse: collapse;
                            border-spacing: 0px;
                          ""
                        >
                          <tr style=""border-collapse: collapse"">
                            <td
                              valign=""top""
                              align=""center""
                              style=""padding: 0; margin: 0; width: 560px""
                            >
                              <table
                                width=""100%""
                                cellspacing=""0""
                                cellpadding=""0""
                                role=""presentation""
                                style=""
                                  mso-table-lspace: 0pt;
                                  mso-table-rspace: 0pt;
                                  border-collapse: collapse;
                                  border-spacing: 0px;
                                ""
                              >
                                <tr style=""border-collapse: collapse"">
                                  <td
                                    class=""made_with""
                                    align=""center""
                                    style=""padding: 0; margin: 0; font-size: 0""
                                  >
                                    <a
                                      target=""_blank""
                                      href=""https://viewstripo.email/?utm_source=templates&utm_medium=email&utm_campaign=car&utm_content=fathers_day""
                                      style=""
                                        -webkit-text-size-adjust: none;
                                        -ms-text-size-adjust: none;
                                        mso-line-height-rule: exactly;
                                        text-decoration: underline;
                                        color: #dbdbdb;
                                        font-size: 14px;
                                      ""
                                      ><img
                                        src=""https://eeslewt.stripocdn.email/content/guids/CABINET_9df86e5b6c53dd0319931e2447ed854b/images/64951510234941531.png""
                                        alt
                                        width=""125""
                                        height=""56""
                                        style=""
                                          display: block;
                                          border: 0;
                                          outline: none;
                                          text-decoration: none;
                                          -ms-interpolation-mode: bicubic;
                                        ""
                                    /></a>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </div>
  </body>
</html>
";
            return htmlContent;
        }
    }

}
