import { CollapseConstructor } from "./collapse";
import { DropdownConstructor } from "./dropdown";
import { ModalConstructor } from "./modal";
import { TabConstructor } from "./tab";
import { TooltipConstructor } from "./tooltip";

interface BootstrapNative {
  Modal: ModalConstructor;
  Collapse: CollapseConstructor;
  Dropdown: DropdownConstructor;
  Tab: TabConstructor;
  Tooltip: TooltipConstructor;
}
declare const BootstrapNative: BootstrapNative;