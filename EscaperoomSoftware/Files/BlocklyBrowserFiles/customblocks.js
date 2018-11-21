Blockly.Blocks['puzzle_state'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Get Puzzle State");
    this.setOutput(true, "Number");
    this.setColour(0);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['puzzle_up'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Puzzle State Up");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(0);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['wait'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Wait");
    this.appendValueInput("seconds")
        .setCheck("Number")
        .appendField("Seconds:");
    this.setInputsInline(true);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(300);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['start'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Start");
    this.setInputsInline(true);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(300);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['stop'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Stop");
    this.setInputsInline(true);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(285);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['get_puzzle_time'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Puzzle Time");
    this.setOutput(true, "Number");
    this.setColour(0);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['get_play_time'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Play Time");
    this.setOutput(true, "Number");
    this.setColour(300);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['doormagnet'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Door magnet");
    this.appendValueInput("macAddress")
        .setCheck("String")
        .appendField("MAC-address:");
    this.appendValueInput("state")
        .setCheck("Boolean")
        .appendField("On/Off:");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(60);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['videoplay'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Play Video");
    this.appendValueInput("macAddress")
        .setCheck("String")
        .appendField("MAC-address:");
    this.appendValueInput("state")
        .setCheck("String")
        .appendField("Video Filename:");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(60);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['doorsensor'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Door Sensor");
    this.appendValueInput("macAddress")
        .setCheck("String")
        .appendField("MAC-address:");
    this.setOutput(true, "Boolean");
    this.setColour(180);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['videocheck'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("is Video Playing");
    this.appendValueInput("macAddress")
        .setCheck("String")
        .appendField("MAC-address:");
    this.setOutput(true, "Boolean");
    this.setColour(180);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};