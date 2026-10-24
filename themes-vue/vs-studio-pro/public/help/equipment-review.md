## Equipment Review

In this article, we will describe the `Equipment Review` and how the process flow works.

### Understanding the Equipment Configuration Toolbar

The Equipment Configuration toolbar contains a collection of various actions to help navigate through the `Equipment Review`. Let's start
by understanding what each button action does.

![Equipment Configuration Toolbar](./help/img/equipment-configuration-toolbar.jpg)

From the image above, left to right.

- Add new equipment configuration
- View equipment instances
- Manage equipment configuration
- Submit equipment configuration for approval
- Final Reviewing (Approving or Rejecting)

These buttons will be disabled based on business rules or missing the administrative role.

### Adding a new Equipment Configuration

Equipment Configuration is a set of ratings measured in amps. Their units are either in Celsius or Fahrenheit. Other attributes of the equipment configuration is;

- Name = a descriptive name of the configuration
- Description = a long description of the configuration
- Type = [Normal, Emergency, Load Dump]
- Time of Day = [Day, Night]

Adding ratings to the equipment configuration is performed in two ways. Manually via the rating form or importing a CSV file.

> Note: A CSV file can only be used on an empty equipment configuration.

Using the rating form consists of the attributes of the rating; _Type, Time of Day, Unit, Temperature, and rating_. All, but the rating fields are drop-down options that are selectable from a list. The rating field must be in a number format, no character allowed.

Once the rating is added to the table. Any mistakes to a rating can be corrected by editing the record in-line of the table row. Clicking the edit icon will place the selected row into edit mode. Make the necessary adjustment to the rating and click the save icon. If you no longer need the selected record within the equipment configuration, you can remove it by clicking the delete icon.

> Note: you will need to click the “Save and Exit” button to apply your changes.

Importing a CSV rating file is a simple and quick way to add ratings to the equipment configuration. A CSV template is available to download within the 'Add Configuration' portion of the equipment configuration. Click the download icon and save the file to your computer. Now open the file using any text editor or Microsoft Excel. The top row is the header and must _not_ be altered or the import may fail. These headers are the attributes of the ratings. Continue to add ratings one per line.

> Note: A CSV file can only be used on an empty equipment configuration.

Once the file is ready, click the import file button with the 'Add Configuration' portion of the configuration. Browse and select your file. Please wait while the file is processed. When completed, you should see a list of ratings populated in the table. Review results and click the “Save and Exit” button to submit ratings to the server.

### Viewing a Equipment Instance

When using an equipment configuration in a circuit group, an instance of that equipment and its ratings are tied to the circuit. Before making changes to an equipment configuration, it would be nice to know what circuits the equipment is connected to.

Clicking the `equipment configuration instance` button will open a popup modal providing the details of the circuit(s) and circuit group(s) that instance is used in.

### Submitting Equipment Configuration for approval

When an equipment configuration is ready for additional approval, clicking this button submits the configuration for review and displays on the dashboard. No further editing should be performed during the “REVIEW” status. Doing so will start a new equipment review process.

> This step of the `Equipment Review` moves the equipment configuration from a “DRAFT” status to a “REVIEW” status.

### Performing a final review (approving or rejecting)

This is the final stage of the `Equipment Review` process. Equipment Configurations that are pending review are listed on the dashboard. Clicking the hyperlink will navigate you to the equipment detail page with the configuration selected. Yellow highlighted records are ratings that were modified or added.

Once the equipment configuration ratings have been reviewed, now it’s time to either approve or reject the configuration. Clicking the “Review” button will expand a floating window with the choice of Approval or Reject along with an optional comment.

Rejecting the equipment review will move the configuration from a “REVIEW” status back to the “DRAFT” status. At this point the user can make the necessary adjustments and resubmit for approval.

Approving the equipment review will move the configuration to one of either statuses; “Complete” or “Emergency”. If the user that created or modified the configuration approves the equipment review, then the configuration is moved to “emergency” status and is available to add to a circuit group. This status will still need additional approval from another user to move to “complete” status.

Otherwise, if the approving user is someone else other than the creator or modifor, the configuration is moved to a “complete” status and is available to add to a circuit group. Nothing else is required and this ends the equipment review process.
