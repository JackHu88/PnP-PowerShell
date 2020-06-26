﻿using OfficeDevPnP.Core.Framework.Graph;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using SharePointPnP.PowerShell.Commands.Base;
using SharePointPnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SharePointPnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Remove, "PnPTeamsChannel")]
    [CmdletHelp("Gets one Office 365 Group (aka Unified Group) or a list of Office 365 Groups. Requires the Azure Active Directory application permission 'Group.Read.All'.",
       Category = CmdletHelpCategory.Graph,
       SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(
      Code = "PS:> Get-PnPUnifiedGroup",
      Remarks = "Retrieves all the Office 365 Groups",
      SortOrder = 1)]
    [CmdletExample(
      Code = "PS:> Get-PnPUnifiedGroup -Identity $groupId",
      Remarks = "Retrieves a specific Office 365 Group based on its ID",
      SortOrder = 2)]
    [CmdletExample(
      Code = "PS:> Get-PnPUnifiedGroup -Identity $groupDisplayName",
      Remarks = "Retrieves a specific or list of Office 365 Groups that start with the given DisplayName",
      SortOrder = 3)]
    [CmdletExample(
      Code = "PS:> Get-PnPUnifiedGroup -Identity $groupSiteMailNickName",
      Remarks = "Retrieves a specific or list of Office 365 Groups for which the email starts with the provided mail nickName",
      SortOrder = 4)]
    [CmdletExample(
      Code = "PS:> Get-PnPUnifiedGroup -Identity $group",
      Remarks = "Retrieves a specific Office 365 Group based on its object instance",
      SortOrder = 5)]
    public class RemoveTeamsChannel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public string GroupId;

        [Parameter(Mandatory = true)]
        public string DisplayName;

        [Parameter(Mandatory = false, HelpMessage = "Specifying the Force parameter will skip the confirmation question.")]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (JwtUtility.HasScope(AccessToken, "Group.Read.All") || JwtUtility.HasScope(AccessToken, "Group.ReadWrite.All"))
            {
                if (ParameterSpecified(nameof(GroupId)))
                {
                    if (Force || ShouldContinue("Removing the channel will also remove all the messages in the channel.", Properties.Resources.Confirm))
                    {
                        TeamsUtility.DeleteChannel(AccessToken, GroupId, DisplayName);
                    }
                }
            }
            else
            {
                WriteWarning("The current access token lacks the Group.ReadWrite.All permission scope");
            }

        }
    }
}
