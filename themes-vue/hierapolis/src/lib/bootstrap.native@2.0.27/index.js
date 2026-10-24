/* eslint-disable */
var commonjsGlobal = typeof globalThis !== "undefined" ? globalThis : typeof window !== "undefined" ? window : typeof global !== "undefined" ? global : typeof self !== "undefined" ? self : {};
function createCommonjsModule(fn, basedir, module) {
  return module = {
    path: basedir,
    exports: {},
    require: function(path, base) {
      return commonjsRequire(path, base === void 0 || base === null ? module.path : base);
    }
  }, fn(module, module.exports), module.exports;
}
function commonjsRequire() {
  throw new Error("Dynamic requires are not currently supported by @rollup/plugin-commonjs");
}
var bootstrapNative = createCommonjsModule(function(module) {
  (function(root, factory) {
    if (module.exports) {
      module.exports = factory();
    } else {
      var bsn = factory();
      root.Affix = bsn.Affix;
      root.Alert = bsn.Alert;
      root.Button = bsn.Button;
      root.Carousel = bsn.Carousel;
      root.Collapse = bsn.Collapse;
      root.Dropdown = bsn.Dropdown;
      root.Modal = bsn.Modal;
      root.Popover = bsn.Popover;
      root.ScrollSpy = bsn.ScrollSpy;
      root.Tab = bsn.Tab;
      root.Tooltip = bsn.Tooltip;
    }
  })(commonjsGlobal, function() {
    var globalObject = typeof commonjsGlobal !== "undefined" ? commonjsGlobal : this || window, DOC = document, HTML = DOC.documentElement, body = "body", BSN = globalObject.BSN = {}, supports = BSN.supports = [], dataToggle = "data-toggle", dataDismiss = "data-dismiss", dataSpy = "data-spy", dataRide = "data-ride", stringAffix = "Affix", stringAlert = "Alert", stringButton = "Button", stringCarousel = "Carousel", stringCollapse = "Collapse", stringDropdown = "Dropdown", stringModal = "Modal", stringPopover = "Popover", stringScrollSpy = "ScrollSpy", stringTab = "Tab", stringTooltip = "Tooltip", databackdrop = "data-backdrop", dataKeyboard = "data-keyboard", dataTarget = "data-target", dataInterval = "data-interval", dataHeight = "data-height", dataPause = "data-pause", dataTitle = "data-title", dataOriginalTitle = "data-original-title", dataOriginalText = "data-original-text", dataDismissible = "data-dismissible", dataTrigger = "data-trigger", dataAnimation = "data-animation", dataContainer = "data-container", dataPlacement = "data-placement", dataDelay = "data-delay", dataOffsetTop = "data-offset-top", dataOffsetBottom = "data-offset-bottom", backdrop = "backdrop", keyboard = "keyboard", delay = "delay", content = "content", target = "target", currentTarget = "currentTarget", interval = "interval", pause = "pause", animation = "animation", placement = "placement", container = "container", offsetTop = "offsetTop", offsetBottom = "offsetBottom", offsetLeft = "offsetLeft", scrollTop = "scrollTop", scrollLeft = "scrollLeft", clientWidth = "clientWidth", clientHeight = "clientHeight", offsetWidth = "offsetWidth", offsetHeight = "offsetHeight", innerWidth = "innerWidth", innerHeight = "innerHeight", scrollHeight = "scrollHeight", height = "height", ariaExpanded = "aria-expanded", ariaHidden = "aria-hidden", clickEvent = "click", focusEvent = "focus", hoverEvent = "hover", keydownEvent = "keydown", keyupEvent = "keyup", resizeEvent = "resize", scrollEvent = "scroll", mouseHover = "onmouseleave" in DOC ? ["mouseenter", "mouseleave"] : ["mouseover", "mouseout"], touchEvents = {start: "touchstart", end: "touchend", move: "touchmove"}, showEvent = "show", shownEvent = "shown", hideEvent = "hide", hiddenEvent = "hidden", closeEvent = "close", closedEvent = "closed", slidEvent = "slid", slideEvent = "slide", changeEvent = "change", getAttribute = "getAttribute", setAttribute = "setAttribute", hasAttribute = "hasAttribute", createElement = "createElement", appendChild = "appendChild", innerHTML = "innerHTML", getElementsByTagName = "getElementsByTagName", preventDefault = "preventDefault", getBoundingClientRect = "getBoundingClientRect", querySelectorAll = "querySelectorAll", getElementsByCLASSNAME = "getElementsByClassName", getComputedStyle = "getComputedStyle", indexOf = "indexOf", parentNode = "parentNode", length = "length", toLowerCase = "toLowerCase", Transition = "Transition", Duration = "Duration", Webkit = "Webkit", style = "style", push = "push", tabindex = "tabindex", contains = "contains", active = "active", inClass = "in", collapsing = "collapsing", disabled = "disabled", loading = "loading", left = "left", right = "right", top = "top", bottom = "bottom", isIE8 = !("opacity" in HTML[style]), tipPositions = /\b(top|bottom|left|right)+/, modalOverlay = 0, fixedTop = "navbar-fixed-top", fixedBottom = "navbar-fixed-bottom", supportTransitions = Webkit + Transition in HTML[style] || Transition[toLowerCase]() in HTML[style], transitionEndEvent = Webkit + Transition in HTML[style] ? Webkit[toLowerCase]() + Transition + "End" : Transition[toLowerCase]() + "end", transitionDuration = Webkit + Duration in HTML[style] ? Webkit[toLowerCase]() + Transition + Duration : Transition[toLowerCase]() + Duration, setFocus = function(element) {
      element.focus ? element.focus() : element.setActive();
    }, addClass = function(element, classNAME) {
      element.classList.add(classNAME);
    }, removeClass = function(element, classNAME) {
      element.classList.remove(classNAME);
    }, hasClass = function(element, classNAME) {
      return element.classList[contains](classNAME);
    }, nodeListToArray = function(nodeList) {
      var childItems = [];
      for (var i = 0, nll = nodeList[length]; i < nll; i++) {
        childItems[push](nodeList[i]);
      }
      return childItems;
    }, getElementsByClassName = function(element, classNAME) {
      var selectionMethod = isIE8 ? querySelectorAll : getElementsByCLASSNAME;
      return nodeListToArray(element[selectionMethod](isIE8 ? "." + classNAME.replace(/\s(?=[a-z])/g, ".") : classNAME));
    }, queryElement = function(selector, parent) {
      var lookUp = parent ? parent : DOC;
      return typeof selector === "object" ? selector : lookUp.querySelector(selector);
    }, getClosest = function(element, selector) {
      var firstChar = selector.charAt(0), selectorSubstring = selector.substr(1);
      if (firstChar === ".") {
        for (; element && element !== DOC; element = element[parentNode]) {
          if (queryElement(selector, element[parentNode]) !== null && hasClass(element, selectorSubstring)) {
            return element;
          }
        }
      } else if (firstChar === "#") {
        for (; element && element !== DOC; element = element[parentNode]) {
          if (element.id === selectorSubstring) {
            return element;
          }
        }
      }
      return false;
    }, on = function(element, event, handler, options) {
      options = options || false;
      element.addEventListener(event, handler, options);
    }, off = function(element, event, handler, options) {
      options = options || false;
      element.removeEventListener(event, handler, options);
    }, one = function(element, event, handler, options) {
      on(element, event, function handlerWrapper(e) {
        handler(e);
        off(element, event, handlerWrapper, options);
      }, options);
    }, supportPassive = function() {
      var result = false;
      try {
        var opts = Object.defineProperty({}, "passive", {
          get: function() {
            result = true;
          }
        });
        one(globalObject, "testPassive", null, opts);
      } catch (e) {
      }
      return result;
    }(), passiveHandler = supportPassive ? {passive: true} : false, getTransitionDurationFromElement = function(element) {
      var duration = supportTransitions ? globalObject[getComputedStyle](element)[transitionDuration] : 0;
      duration = parseFloat(duration);
      duration = typeof duration === "number" && !isNaN(duration) ? duration * 1e3 : 0;
      return duration;
    }, emulateTransitionEnd = function(element, handler) {
      var called = 0, duration = getTransitionDurationFromElement(element);
      duration ? one(element, transitionEndEvent, function(e) {
        !called && handler(e), called = 1;
      }) : setTimeout(function() {
        !called && handler(), called = 1;
      }, 17);
    }, bootstrapCustomEvent = function(eventName, componentName, related) {
      var OriginalCustomEvent = new CustomEvent(eventName + ".bs." + componentName);
      OriginalCustomEvent.relatedTarget = related;
      this.dispatchEvent(OriginalCustomEvent);
    }, getScroll = function() {
      return {
        y: globalObject.pageYOffset || HTML[scrollTop],
        x: globalObject.pageXOffset || HTML[scrollLeft]
      };
    }, styleTip = function(link, element, position, parent) {
      var elementDimensions = {w: element[offsetWidth], h: element[offsetHeight]}, windowWidth = HTML[clientWidth] || DOC[body][clientWidth], windowHeight = HTML[clientHeight] || DOC[body][clientHeight], rect = link[getBoundingClientRect](), scroll = parent === DOC[body] ? getScroll() : {x: parent[offsetLeft] + parent[scrollLeft], y: parent[offsetTop] + parent[scrollTop]}, linkDimensions = {w: rect[right] - rect[left], h: rect[bottom] - rect[top]}, arrow = queryElement('[class*="arrow"]', element), topPosition, leftPosition, arrowTop, arrowLeft, halfTopExceed = rect[top] + linkDimensions.h / 2 - elementDimensions.h / 2 < 0, halfLeftExceed = rect[left] + linkDimensions.w / 2 - elementDimensions.w / 2 < 0, halfRightExceed = rect[left] + elementDimensions.w / 2 + linkDimensions.w / 2 >= windowWidth, halfBottomExceed = rect[top] + elementDimensions.h / 2 + linkDimensions.h / 2 >= windowHeight, topExceed = rect[top] - elementDimensions.h < 0, leftExceed = rect[left] - elementDimensions.w < 0, bottomExceed = rect[top] + elementDimensions.h + linkDimensions.h >= windowHeight, rightExceed = rect[left] + elementDimensions.w + linkDimensions.w >= windowWidth;
      position = (position === left || position === right) && leftExceed && rightExceed ? top : position;
      position = position === top && topExceed ? bottom : position;
      position = position === bottom && bottomExceed ? top : position;
      position = position === left && leftExceed ? right : position;
      position = position === right && rightExceed ? left : position;
      if (position === left || position === right) {
        if (position === left) {
          leftPosition = rect[left] + scroll.x - elementDimensions.w;
        } else {
          leftPosition = rect[left] + scroll.x + linkDimensions.w;
        }
        if (halfTopExceed) {
          topPosition = rect[top] + scroll.y;
          arrowTop = linkDimensions.h / 2;
        } else if (halfBottomExceed) {
          topPosition = rect[top] + scroll.y - elementDimensions.h + linkDimensions.h;
          arrowTop = elementDimensions.h - linkDimensions.h / 2;
        } else {
          topPosition = rect[top] + scroll.y - elementDimensions.h / 2 + linkDimensions.h / 2;
        }
      } else if (position === top || position === bottom) {
        if (position === top) {
          topPosition = rect[top] + scroll.y - elementDimensions.h;
        } else {
          topPosition = rect[top] + scroll.y + linkDimensions.h;
        }
        if (halfLeftExceed) {
          leftPosition = 0;
          arrowLeft = rect[left] + linkDimensions.w / 2;
        } else if (halfRightExceed) {
          leftPosition = windowWidth - elementDimensions.w * 1.01;
          arrowLeft = elementDimensions.w - (windowWidth - rect[left]) + linkDimensions.w / 2;
        } else {
          leftPosition = rect[left] + scroll.x - elementDimensions.w / 2 + linkDimensions.w / 2;
        }
      }
      element[style][top] = topPosition + "px";
      element[style][left] = leftPosition + "px";
      arrowTop && (arrow[style][top] = arrowTop + "px");
      arrowLeft && (arrow[style][left] = arrowLeft + "px");
      element.className[indexOf](position) === -1 && (element.className = element.className.replace(tipPositions, position));
    };
    BSN.version = "2.0.27";
    var Affix = function(element, options) {
      element = queryElement(element);
      options = options || {};
      var targetData = element[getAttribute](dataTarget), offsetTopData = element[getAttribute](dataOffsetTop), offsetBottomData = element[getAttribute](dataOffsetBottom), affix = "affix", affixed = "affixed", fn = "function", update = "update", affixTop = "affix-top", affixedTop = "affixed-top", affixBottom = "affix-bottom", affixedBottom = "affixed-bottom";
      this[target] = options[target] ? queryElement(options[target]) : queryElement(targetData) || null;
      this[offsetTop] = options[offsetTop] ? options[offsetTop] : parseInt(offsetTopData) || 0;
      this[offsetBottom] = options[offsetBottom] ? options[offsetBottom] : parseInt(offsetBottomData) || 0;
      if (!this[target] && !(this[offsetTop] || this[offsetBottom])) {
        return;
      }
      var self2 = this, pinOffsetTop, pinOffsetBottom, maxScroll, scrollY, pinnedTop, pinnedBottom, affixedToTop = false, affixedToBottom = false, getMaxScroll = function() {
        return Math.max(DOC[body][scrollHeight], DOC[body][offsetHeight], HTML[clientHeight], HTML[scrollHeight], HTML[offsetHeight]);
      }, getOffsetTop = function() {
        if (self2[target] !== null) {
          return self2[target][getBoundingClientRect]()[top] + scrollY;
        } else if (self2[offsetTop]) {
          return parseInt(typeof self2[offsetTop] === fn ? self2[offsetTop]() : self2[offsetTop] || 0);
        }
      }, getOffsetBottom = function() {
        if (self2[offsetBottom]) {
          return maxScroll - element[offsetHeight] - parseInt(typeof self2[offsetBottom] === fn ? self2[offsetBottom]() : self2[offsetBottom] || 0);
        }
      }, checkPosition = function() {
        maxScroll = getMaxScroll();
        scrollY = parseInt(getScroll().y, 0);
        pinOffsetTop = getOffsetTop();
        pinOffsetBottom = getOffsetBottom();
        pinnedTop = parseInt(pinOffsetTop) - scrollY < 0 && scrollY > parseInt(pinOffsetTop);
        pinnedBottom = parseInt(pinOffsetBottom) - scrollY < 0 && scrollY > parseInt(pinOffsetBottom);
      }, pinTop = function() {
        if (!affixedToTop && !hasClass(element, affix)) {
          bootstrapCustomEvent.call(element, affix, affix);
          bootstrapCustomEvent.call(element, affixTop, affix);
          addClass(element, affix);
          affixedToTop = true;
          bootstrapCustomEvent.call(element, affixed, affix);
          bootstrapCustomEvent.call(element, affixedTop, affix);
        }
      }, unPinTop = function() {
        if (affixedToTop && hasClass(element, affix)) {
          removeClass(element, affix);
          affixedToTop = false;
        }
      }, pinBottom = function() {
        if (!affixedToBottom && !hasClass(element, affixBottom)) {
          bootstrapCustomEvent.call(element, affix, affix);
          bootstrapCustomEvent.call(element, affixBottom, affix);
          addClass(element, affixBottom);
          affixedToBottom = true;
          bootstrapCustomEvent.call(element, affixed, affix);
          bootstrapCustomEvent.call(element, affixedBottom, affix);
        }
      }, unPinBottom = function() {
        if (affixedToBottom && hasClass(element, affixBottom)) {
          removeClass(element, affixBottom);
          affixedToBottom = false;
        }
      }, updatePin = function() {
        if (pinnedBottom) {
          if (pinnedTop) {
            unPinTop();
          }
          pinBottom();
        } else {
          unPinBottom();
          if (pinnedTop) {
            pinTop();
          } else {
            unPinTop();
          }
        }
      };
      this[update] = function() {
        checkPosition();
        updatePin();
      };
      if (!(stringAffix in element)) {
        on(globalObject, scrollEvent, self2[update], passiveHandler);
        !isIE8 && on(globalObject, resizeEvent, self2[update], passiveHandler);
      }
      element[stringAffix] = self2;
      self2[update]();
    };
    supports[push]([stringAffix, Affix, "[" + dataSpy + '="affix"]']);
    var Alert = function(element) {
      element = queryElement(element);
      var self2 = this, component = "alert", alert = getClosest(element, "." + component), triggerHandler = function() {
        hasClass(alert, "fade") ? emulateTransitionEnd(alert, transitionEndHandler) : transitionEndHandler();
      }, clickHandler = function(e) {
        alert = getClosest(e[target], "." + component);
        element = queryElement("[" + dataDismiss + '="' + component + '"]', alert);
        element && alert && (element === e[target] || element[contains](e[target])) && self2.close();
      }, transitionEndHandler = function() {
        bootstrapCustomEvent.call(alert, closedEvent, component);
        off(element, clickEvent, clickHandler);
        alert[parentNode].removeChild(alert);
      };
      this.close = function() {
        if (alert && element && hasClass(alert, inClass)) {
          bootstrapCustomEvent.call(alert, closeEvent, component);
          removeClass(alert, inClass);
          alert && triggerHandler();
        }
      };
      if (!(stringAlert in element)) {
        on(element, clickEvent, clickHandler);
      }
      element[stringAlert] = self2;
    };
    supports[push]([stringAlert, Alert, "[" + dataDismiss + '="alert"]']);
    var Button = function(element, option) {
      element = queryElement(element);
      option = option || null;
      var toggled = false, component = "button", checked = "checked", reset = "reset", LABEL = "LABEL", INPUT = "INPUT", setState = function() {
        if (!!option && option !== reset) {
          if (option === loading) {
            addClass(element, disabled);
            element[setAttribute](disabled, disabled);
            element[setAttribute](dataOriginalText, element[innerHTML].trim());
          }
          element[innerHTML] = element[getAttribute]("data-" + option + "-text");
        }
      }, resetState = function() {
        if (element[getAttribute](dataOriginalText)) {
          if (hasClass(element, disabled) || element[getAttribute](disabled) === disabled) {
            removeClass(element, disabled);
            element.removeAttribute(disabled);
          }
          element[innerHTML] = element[getAttribute](dataOriginalText);
        }
      }, keyHandler = function(e) {
        var key = e.which || e.keyCode;
        key === 32 && e[target] === DOC.activeElement && toggle(e);
      }, preventScroll = function(e) {
        var key = e.which || e.keyCode;
        key === 32 && e[preventDefault]();
      }, toggle = function(e) {
        var label = e[target].tagName === LABEL ? e[target] : e[target][parentNode].tagName === LABEL ? e[target][parentNode] : null;
        if (!label)
          return;
        var labels = getElementsByClassName(label[parentNode], "btn"), input = label[getElementsByTagName](INPUT)[0];
        if (!input)
          return;
        if (input.type === "checkbox") {
          if (!input[checked]) {
            addClass(label, active);
            input[getAttribute](checked);
            input[setAttribute](checked, checked);
            input[checked] = true;
          } else {
            removeClass(label, active);
            input[getAttribute](checked);
            input.removeAttribute(checked);
            input[checked] = false;
          }
          if (!toggled) {
            toggled = true;
            bootstrapCustomEvent.call(input, changeEvent, component);
            bootstrapCustomEvent.call(element, changeEvent, component);
          }
        }
        if (input.type === "radio" && !toggled) {
          if (!input[checked] || e.screenX === 0 && e.screenY == 0) {
            addClass(label, active);
            input[setAttribute](checked, checked);
            input[checked] = true;
            bootstrapCustomEvent.call(input, changeEvent, component);
            bootstrapCustomEvent.call(element, changeEvent, component);
            toggled = true;
            for (var i2 = 0, ll = labels[length]; i2 < ll; i2++) {
              var otherLabel = labels[i2], otherInput = otherLabel[getElementsByTagName](INPUT)[0];
              if (otherLabel !== label && hasClass(otherLabel, active)) {
                removeClass(otherLabel, active);
                otherInput.removeAttribute(checked);
                otherInput[checked] = false;
                bootstrapCustomEvent.call(otherInput, changeEvent, component);
              }
            }
          }
        }
        setTimeout(function() {
          toggled = false;
        }, 50);
      };
      if (hasClass(element, "btn")) {
        if (option !== null) {
          if (option !== reset) {
            setState();
          } else {
            resetState();
          }
        }
      } else {
        if (!(stringButton in element)) {
          on(element, clickEvent, toggle);
          on(element, keyupEvent, keyHandler), on(element, keydownEvent, preventScroll);
        }
        var labelsToACtivate = getElementsByClassName(element, "btn"), lbll = labelsToACtivate[length];
        for (var i = 0; i < lbll; i++) {
          !hasClass(labelsToACtivate[i], active) && queryElement("input", labelsToACtivate[i])[getAttribute](checked) && addClass(labelsToACtivate[i], active);
        }
        element[stringButton] = this;
      }
    };
    supports[push]([stringButton, Button, "[" + dataToggle + '="buttons"]']);
    var Carousel = function(element, options) {
      element = queryElement(element);
      options = options || {};
      var intervalAttribute = element[getAttribute](dataInterval), intervalOption = options[interval], intervalData = intervalAttribute === "false" ? 0 : parseInt(intervalAttribute), pauseData = element[getAttribute](dataPause) === hoverEvent || false, keyboardData = element[getAttribute](dataKeyboard) === "true" || false, component = "carousel", paused = "paused", direction = "direction", dataSlideTo = "data-slide-to";
      this[keyboard] = options[keyboard] === true || keyboardData;
      this[pause] = options[pause] === hoverEvent || pauseData ? hoverEvent : false;
      this[interval] = typeof intervalOption === "number" ? intervalOption : intervalOption === false || intervalData === 0 || intervalData === false ? 0 : isNaN(intervalData) ? 5e3 : intervalData;
      var self2 = this, index = element.index = 0, timer = element.timer = 0, isSliding = false, isTouch = false, startXPosition = null, currentXPosition = null, endXPosition = null, slides = getElementsByClassName(element, "item"), total = slides[length], slideDirection = this[direction] = left, controls = getElementsByClassName(element, component + "-control"), leftArrow = controls[0], rightArrow = controls[1], indicator = queryElement("." + component + "-indicators", element), indicators = indicator && indicator[getElementsByTagName]("LI") || [];
      if (total < 2) {
        return;
      }
      var pauseHandler = function() {
        if (self2[interval] !== false && !hasClass(element, paused)) {
          addClass(element, paused);
          !isSliding && (clearInterval(timer), timer = null);
        }
      }, resumeHandler = function() {
        if (self2[interval] !== false && hasClass(element, paused)) {
          removeClass(element, paused);
          !isSliding && (clearInterval(timer), timer = null);
          !isSliding && self2.cycle();
        }
      }, indicatorHandler = function(e) {
        e[preventDefault]();
        if (isSliding)
          return;
        var eventTarget = e[target];
        if (eventTarget && !hasClass(eventTarget, active) && eventTarget[getAttribute](dataSlideTo)) {
          index = parseInt(eventTarget[getAttribute](dataSlideTo), 10);
        } else {
          return false;
        }
        self2.slideTo(index);
      }, controlsHandler = function(e) {
        e[preventDefault]();
        if (isSliding)
          return;
        var eventTarget = e.currentTarget || e.srcElement;
        if (eventTarget === rightArrow) {
          index++;
        } else if (eventTarget === leftArrow) {
          index--;
        }
        self2.slideTo(index);
      }, keyHandler = function(e) {
        if (isSliding)
          return;
        switch (e.which) {
          case 39:
            index++;
            break;
          case 37:
            index--;
            break;
          default:
            return;
        }
        self2.slideTo(index);
      }, toggleTouchEvents = function(toggle) {
        toggle(element, touchEvents.move, touchMoveHandler, passiveHandler);
        toggle(element, touchEvents.end, touchEndHandler, passiveHandler);
      }, touchDownHandler = function(e) {
        if (isTouch) {
          return;
        }
        startXPosition = parseInt(e.touches[0].pageX);
        if (element.contains(e[target])) {
          isTouch = true;
          toggleTouchEvents(on);
        }
      }, touchMoveHandler = function(e) {
        if (!isTouch) {
          e.preventDefault();
          return;
        }
        currentXPosition = parseInt(e.touches[0].pageX);
        if (e.type === "touchmove" && e.touches[length] > 1) {
          e.preventDefault();
          return false;
        }
      }, touchEndHandler = function(e) {
        if (!isTouch || isSliding) {
          return;
        }
        endXPosition = currentXPosition || parseInt(e.touches[0].pageX);
        if (isTouch) {
          if ((!element.contains(e[target]) || !element.contains(e.relatedTarget)) && Math.abs(startXPosition - endXPosition) < 75) {
            return false;
          } else {
            if (currentXPosition < startXPosition) {
              index++;
            } else if (currentXPosition > startXPosition) {
              index--;
            }
            isTouch = false;
            self2.slideTo(index);
          }
          toggleTouchEvents(off);
        }
      }, isElementInScrollRange = function() {
        var rect = element[getBoundingClientRect](), viewportHeight = globalObject[innerHeight] || HTML[clientHeight];
        return rect[top] <= viewportHeight && rect[bottom] >= 0;
      }, setActivePage = function(pageIndex) {
        for (var i = 0, icl = indicators[length]; i < icl; i++) {
          removeClass(indicators[i], active);
        }
        if (indicators[pageIndex])
          addClass(indicators[pageIndex], active);
      };
      this.cycle = function() {
        if (timer) {
          clearInterval(timer);
          timer = null;
        }
        timer = setInterval(function() {
          isElementInScrollRange() && (index++, self2.slideTo(index));
        }, this[interval]);
      };
      this.slideTo = function(next) {
        if (isSliding)
          return;
        var activeItem = this.getActiveIndex(), orientation;
        if (activeItem === next) {
          return;
        } else if (activeItem < next || activeItem === 0 && next === total - 1) {
          slideDirection = self2[direction] = left;
        } else if (activeItem > next || activeItem === total - 1 && next === 0) {
          slideDirection = self2[direction] = right;
        }
        if (next < 0) {
          next = total - 1;
        } else if (next >= total) {
          next = 0;
        }
        index = next;
        orientation = slideDirection === left ? "next" : "prev";
        bootstrapCustomEvent.call(element, slideEvent, component, slides[next]);
        isSliding = true;
        clearInterval(timer);
        timer = null;
        setActivePage(next);
        if (supportTransitions && hasClass(element, "slide")) {
          addClass(slides[next], orientation);
          slides[next][offsetWidth];
          addClass(slides[next], slideDirection);
          addClass(slides[activeItem], slideDirection);
          emulateTransitionEnd(slides[next], function(e) {
            var timeout = e && e[target] !== slides[next] ? e.elapsedTime * 1e3 + 100 : 20;
            isSliding && setTimeout(function() {
              isSliding = false;
              addClass(slides[next], active);
              removeClass(slides[activeItem], active);
              removeClass(slides[next], orientation);
              removeClass(slides[next], slideDirection);
              removeClass(slides[activeItem], slideDirection);
              bootstrapCustomEvent.call(element, slidEvent, component, slides[next]);
              if (self2[interval] && !hasClass(element, paused)) {
                self2.cycle();
              }
            }, timeout);
          });
        } else {
          addClass(slides[next], active);
          slides[next][offsetWidth];
          removeClass(slides[activeItem], active);
          setTimeout(function() {
            isSliding = false;
            if (self2[interval] && !hasClass(element, paused)) {
              self2.cycle();
            }
            bootstrapCustomEvent.call(element, slidEvent, component, slides[next]);
          }, 100);
        }
      };
      this.getActiveIndex = function() {
        return slides[indexOf](getElementsByClassName(element, "item active")[0]) || 0;
      };
      if (!(stringCarousel in element)) {
        if (self2[pause] && self2[interval]) {
          on(element, mouseHover[0], pauseHandler);
          on(element, mouseHover[1], resumeHandler);
          on(element, touchEvents.start, pauseHandler, passiveHandler);
          on(element, touchEvents.end, resumeHandler, passiveHandler);
        }
        slides[length] > 1 && on(element, touchEvents.start, touchDownHandler, passiveHandler);
        rightArrow && on(rightArrow, clickEvent, controlsHandler);
        leftArrow && on(leftArrow, clickEvent, controlsHandler);
        indicator && on(indicator, clickEvent, indicatorHandler);
        self2[keyboard] && on(globalObject, keydownEvent, keyHandler);
      }
      if (self2.getActiveIndex() < 0) {
        slides[length] && addClass(slides[0], active);
        indicators[length] && setActivePage(0);
      }
      if (self2[interval]) {
        self2.cycle();
      }
      element[stringCarousel] = self2;
    };
    supports[push]([stringCarousel, Carousel, "[" + dataRide + '="carousel"]']);
    var Collapse = function(element, options) {
      element = queryElement(element);
      options = options || {};
      var accordion = null, collapse = null, self2 = this, accordionData = element[getAttribute]("data-parent"), activeCollapse, activeElement, component = "collapse", collapsed = "collapsed", isAnimating = "isAnimating", openAction = function(collapseElement, toggle) {
        bootstrapCustomEvent.call(collapseElement, showEvent, component);
        collapseElement[isAnimating] = true;
        addClass(collapseElement, collapsing);
        removeClass(collapseElement, component);
        collapseElement[style][height] = collapseElement[scrollHeight] + "px";
        emulateTransitionEnd(collapseElement, function() {
          collapseElement[isAnimating] = false;
          collapseElement[setAttribute](ariaExpanded, "true");
          toggle[setAttribute](ariaExpanded, "true");
          removeClass(collapseElement, collapsing);
          addClass(collapseElement, component);
          addClass(collapseElement, inClass);
          collapseElement[style][height] = "";
          bootstrapCustomEvent.call(collapseElement, shownEvent, component);
        });
      }, closeAction = function(collapseElement, toggle) {
        bootstrapCustomEvent.call(collapseElement, hideEvent, component);
        collapseElement[isAnimating] = true;
        collapseElement[style][height] = collapseElement[scrollHeight] + "px";
        removeClass(collapseElement, component);
        removeClass(collapseElement, inClass);
        addClass(collapseElement, collapsing);
        collapseElement[offsetWidth];
        collapseElement[style][height] = "0px";
        emulateTransitionEnd(collapseElement, function() {
          collapseElement[isAnimating] = false;
          collapseElement[setAttribute](ariaExpanded, "false");
          toggle[setAttribute](ariaExpanded, "false");
          removeClass(collapseElement, collapsing);
          addClass(collapseElement, component);
          collapseElement[style][height] = "";
          bootstrapCustomEvent.call(collapseElement, hiddenEvent, component);
        });
      }, getTarget = function() {
        var href = element.href && element[getAttribute]("href"), parent = element[getAttribute](dataTarget), id = href || parent && parent.charAt(0) === "#" && parent;
        return id && queryElement(id);
      };
      this.toggle = function(e) {
        e[preventDefault]();
        if (!hasClass(collapse, inClass)) {
          self2.show();
        } else {
          self2.hide();
        }
      };
      this.hide = function() {
        if (collapse[isAnimating])
          return;
        closeAction(collapse, element);
        addClass(element, collapsed);
      };
      this.show = function() {
        if (accordion) {
          activeCollapse = queryElement("." + component + "." + inClass, accordion);
          activeElement = activeCollapse && (queryElement("[" + dataTarget + '="#' + activeCollapse.id + '"]', accordion) || queryElement('[href="#' + activeCollapse.id + '"]', accordion));
        }
        if (!collapse[isAnimating] || activeCollapse && !activeCollapse[isAnimating]) {
          if (activeElement && activeCollapse !== collapse) {
            closeAction(activeCollapse, activeElement);
            addClass(activeElement, collapsed);
          }
          openAction(collapse, element);
          removeClass(element, collapsed);
        }
      };
      if (!(stringCollapse in element)) {
        on(element, clickEvent, self2.toggle);
      }
      collapse = getTarget();
      collapse[isAnimating] = false;
      accordion = queryElement(options.parent) || accordionData && getClosest(element, accordionData);
      element[stringCollapse] = self2;
    };
    supports[push]([stringCollapse, Collapse, "[" + dataToggle + '="collapse"]']);
    var Dropdown = function(element, option) {
      element = queryElement(element);
      this.persist = option === true || element[getAttribute]("data-persist") === "true" || false;
      var self2 = this, children = "children", parent = element[parentNode], component = "dropdown", open = "open", relatedTarget = null, menu = queryElement(".dropdown-menu", parent), menuItems = function() {
        var set = menu[children], newSet = [];
        for (var i = 0; i < set[length]; i++) {
          set[i][children][length] && (set[i][children][0].tagName === "A" && newSet[push](set[i]));
        }
        return newSet;
      }(), preventEmptyAnchor = function(anchor) {
        (anchor.href && anchor.href.slice(-1) === "#" || anchor[parentNode] && anchor[parentNode].href && anchor[parentNode].href.slice(-1) === "#") && this[preventDefault]();
      }, toggleDismiss = function() {
        var type = element[open] ? on : off;
        type(DOC, clickEvent, dismissHandler);
        type(DOC, keydownEvent, preventScroll);
        type(DOC, keyupEvent, keyHandler);
        type(DOC, focusEvent, dismissHandler, true);
      }, dismissHandler = function(e) {
        var eventTarget = e[target], hasData = eventTarget && (eventTarget[getAttribute](dataToggle) || eventTarget[parentNode] && getAttribute in eventTarget[parentNode] && eventTarget[parentNode][getAttribute](dataToggle));
        if (e.type === focusEvent && (eventTarget === element || eventTarget === menu || menu[contains](eventTarget))) {
          return;
        }
        if ((eventTarget === menu || menu[contains](eventTarget)) && (self2.persist || hasData)) {
          return;
        } else {
          relatedTarget = eventTarget === element || element[contains](eventTarget) ? element : null;
          hide();
        }
        preventEmptyAnchor.call(e, eventTarget);
      }, clickHandler = function(e) {
        relatedTarget = element;
        show();
        preventEmptyAnchor.call(e, e[target]);
      }, preventScroll = function(e) {
        var key = e.which || e.keyCode;
        if (key === 38 || key === 40) {
          e[preventDefault]();
        }
      }, keyHandler = function(e) {
        var key = e.which || e.keyCode, activeItem = DOC.activeElement, idx = menuItems[indexOf](activeItem[parentNode]), isSameElement = activeItem === element, isInsideMenu = menu[contains](activeItem), isMenuItem = activeItem[parentNode][parentNode] === menu;
        if (isMenuItem) {
          idx = isSameElement ? 0 : key === 38 ? idx > 1 ? idx - 1 : 0 : key === 40 ? idx < menuItems[length] - 1 ? idx + 1 : idx : idx;
          menuItems[idx] && setFocus(menuItems[idx][children][0]);
        }
        if ((menuItems[length] && isMenuItem || !menuItems[length] && (isInsideMenu || isSameElement) || !isInsideMenu) && element[open] && key === 27) {
          self2.toggle();
          relatedTarget = null;
        }
      }, show = function() {
        bootstrapCustomEvent.call(parent, showEvent, component, relatedTarget);
        addClass(parent, open);
        element[setAttribute](ariaExpanded, true);
        bootstrapCustomEvent.call(parent, shownEvent, component, relatedTarget);
        element[open] = true;
        off(element, clickEvent, clickHandler);
        setTimeout(function() {
          setFocus(menu[getElementsByTagName]("INPUT")[0] || element);
          toggleDismiss();
        }, 1);
      }, hide = function() {
        bootstrapCustomEvent.call(parent, hideEvent, component, relatedTarget);
        removeClass(parent, open);
        element[setAttribute](ariaExpanded, false);
        bootstrapCustomEvent.call(parent, hiddenEvent, component, relatedTarget);
        element[open] = false;
        toggleDismiss();
        setFocus(element);
        setTimeout(function() {
          on(element, clickEvent, clickHandler);
        }, 1);
      };
      element[open] = false;
      this.toggle = function() {
        if (hasClass(parent, open) && element[open]) {
          hide();
        } else {
          show();
        }
      };
      if (!(stringDropdown in element)) {
        !tabindex in menu && menu[setAttribute](tabindex, "0");
        on(element, clickEvent, clickHandler);
      }
      element[stringDropdown] = self2;
    };
    supports[push]([stringDropdown, Dropdown, "[" + dataToggle + '="dropdown"]']);
    var Modal = function(element, options) {
      element = queryElement(element);
      var component = "modal", staticString = "static", modalTrigger = "modalTrigger", paddingRight = "paddingRight", modalBackdropString = "modal-backdrop", isAnimating = "isAnimating", btnCheck = element[getAttribute](dataTarget) || element[getAttribute]("href"), checkModal = queryElement(btnCheck), modal = hasClass(element, component) ? element : checkModal;
      if (hasClass(element, component)) {
        element = null;
      }
      if (!modal) {
        return;
      }
      options = options || {};
      this[keyboard] = options[keyboard] === false || modal[getAttribute](dataKeyboard) === "false" ? false : true;
      this[backdrop] = options[backdrop] === staticString || modal[getAttribute](databackdrop) === staticString ? staticString : true;
      this[backdrop] = options[backdrop] === false || modal[getAttribute](databackdrop) === "false" ? false : this[backdrop];
      this[animation] = hasClass(modal, "fade") ? true : false;
      this[content] = options[content];
      modal[isAnimating] = false;
      var self2 = this, relatedTarget = null, bodyIsOverflowing, scrollBarWidth, overlay, overlayDelay, modalTimer, fixedItems = getElementsByClassName(HTML, fixedTop).concat(getElementsByClassName(HTML, fixedBottom)), getWindowWidth = function() {
        var htmlRect = HTML[getBoundingClientRect]();
        return globalObject[innerWidth] || htmlRect[right] - Math.abs(htmlRect[left]);
      }, setScrollbar = function() {
        var bodyStyle = DOC[body].currentStyle || globalObject[getComputedStyle](DOC[body]), bodyPad = parseInt(bodyStyle[paddingRight], 10), itemPad;
        if (bodyIsOverflowing) {
          DOC[body][style][paddingRight] = bodyPad + scrollBarWidth + "px";
          modal[style][paddingRight] = scrollBarWidth + "px";
          if (fixedItems[length]) {
            for (var i = 0; i < fixedItems[length]; i++) {
              itemPad = (fixedItems[i].currentStyle || globalObject[getComputedStyle](fixedItems[i]))[paddingRight];
              fixedItems[i][style][paddingRight] = parseInt(itemPad) + scrollBarWidth + "px";
            }
          }
        }
      }, resetScrollbar = function() {
        DOC[body][style][paddingRight] = "";
        modal[style][paddingRight] = "";
        if (fixedItems[length]) {
          for (var i = 0; i < fixedItems[length]; i++) {
            fixedItems[i][style][paddingRight] = "";
          }
        }
      }, measureScrollbar = function() {
        var scrollDiv = DOC[createElement]("div"), widthValue;
        scrollDiv.className = component + "-scrollbar-measure";
        DOC[body][appendChild](scrollDiv);
        widthValue = scrollDiv[offsetWidth] - scrollDiv[clientWidth];
        DOC[body].removeChild(scrollDiv);
        return widthValue;
      }, checkScrollbar = function() {
        bodyIsOverflowing = DOC[body][clientWidth] < getWindowWidth();
        scrollBarWidth = measureScrollbar();
      }, createOverlay = function() {
        var newOverlay = DOC[createElement]("div");
        overlay = queryElement("." + modalBackdropString);
        if (overlay === null) {
          newOverlay[setAttribute]("class", modalBackdropString + (self2[animation] ? " fade" : ""));
          overlay = newOverlay;
          DOC[body][appendChild](overlay);
        }
        modalOverlay = 1;
      }, removeOverlay = function() {
        overlay = queryElement("." + modalBackdropString);
        if (overlay && overlay !== null && typeof overlay === "object") {
          modalOverlay = 0;
          DOC[body].removeChild(overlay);
          overlay = null;
        }
      }, triggerShow = function() {
        setFocus(modal);
        modal[isAnimating] = false;
        bootstrapCustomEvent.call(modal, shownEvent, component, relatedTarget);
        on(globalObject, resizeEvent, self2.update, passiveHandler);
        on(modal, clickEvent, dismissHandler);
        on(DOC, keydownEvent, keyHandler);
      }, triggerHide = function() {
        modal[style].display = "";
        element && setFocus(element);
        bootstrapCustomEvent.call(modal, hiddenEvent, component);
        (function() {
          if (!getElementsByClassName(DOC, component + " " + inClass)[0]) {
            resetScrollbar();
            removeClass(DOC[body], component + "-open");
            overlay && hasClass(overlay, "fade") ? (removeClass(overlay, inClass), emulateTransitionEnd(overlay, removeOverlay)) : removeOverlay();
            off(globalObject, resizeEvent, self2.update, passiveHandler);
            off(modal, clickEvent, dismissHandler);
            off(DOC, keydownEvent, keyHandler);
          }
        })();
        modal[isAnimating] = false;
      }, clickHandler = function(e) {
        if (modal[isAnimating])
          return;
        var clickTarget = e[target];
        clickTarget = clickTarget[hasAttribute](dataTarget) || clickTarget[hasAttribute]("href") ? clickTarget : clickTarget[parentNode];
        if (clickTarget === element && !hasClass(modal, inClass)) {
          modal[modalTrigger] = element;
          relatedTarget = element;
          self2.show();
          e[preventDefault]();
        }
      }, keyHandler = function(e) {
        if (modal[isAnimating])
          return;
        var key = e.which || e.keyCode;
        if (self2[keyboard] && key == 27 && hasClass(modal, inClass)) {
          self2.hide();
        }
      }, dismissHandler = function(e) {
        if (modal[isAnimating])
          return;
        var clickTarget = e[target];
        if (hasClass(modal, inClass) && (clickTarget[parentNode][getAttribute](dataDismiss) === component || clickTarget[getAttribute](dataDismiss) === component || clickTarget === modal && self2[backdrop] !== staticString)) {
          self2.hide();
          relatedTarget = null;
          e[preventDefault]();
        }
      };
      this.toggle = function() {
        if (hasClass(modal, inClass)) {
          this.hide();
        } else {
          this.show();
        }
      };
      this.show = function() {
        if (hasClass(modal, inClass) || modal[isAnimating]) {
          return;
        }
        clearTimeout(modalTimer);
        modalTimer = setTimeout(function() {
          modal[isAnimating] = true;
          bootstrapCustomEvent.call(modal, showEvent, component, relatedTarget);
          /** Jeremy Travis 10-30-19:
          * comment this out due to effecting modals by hiding the parent modal in a event a sub-child modal appears
          */
          // var currentOpen = getElementsByClassName(DOC, component + " in")[0];
          // if (currentOpen && currentOpen !== modal) {
          //   modalTrigger in currentOpen && currentOpen[modalTrigger][stringModal].hide();
          //   stringModal in currentOpen && currentOpen[stringModal].hide();
          // }
          if (self2[backdrop]) {
            !modalOverlay && !overlay && createOverlay();
          }
          if (overlay && !hasClass(overlay, inClass)) {
            overlay[offsetWidth];
            overlayDelay = getTransitionDurationFromElement(overlay);
            addClass(overlay, inClass);
          }
          setTimeout(function() {
            /** Jeremy Travis 10-30-2019:
            * Check if Vue instance is attached, if so, run the Modal-Picker's Open() command
            */
            //if (commonjsGlobal.$vm) {
              // console.log(commonjsGlobal);
            //}
          //  console.log(JSON.parse(JSON.stringify(modal)));
          //   if (modal.__vue__) {
          //     let vm = modal.__vue__;                
          //     if (vm.master !== undefined && vm.multi !== undefined) {
          //         vm.Open();

          //         //clear all vaildation upon opening
          //         vm.$store.commit("vaildation/Errors", []);
          //     }
          //   }
            modal[style].display = "block";
            checkScrollbar();
            setScrollbar();
            addClass(DOC[body], component + "-open");
            addClass(modal, inClass);
            modal[setAttribute](ariaHidden, false);
            hasClass(modal, "fade") ? emulateTransitionEnd(modal, triggerShow) : triggerShow();
          }, supportTransitions && overlay && overlayDelay ? overlayDelay : 1);
        }, 1);
      };
      this.hide = function() {
        if (modal[isAnimating] || !hasClass(modal, inClass)) {
          return;
        }
        clearTimeout(modalTimer);
        modalTimer = setTimeout(function() {
          modal[isAnimating] = true;
          bootstrapCustomEvent.call(modal, hideEvent, component);
          overlay = queryElement("." + modalBackdropString);
          overlayDelay = overlay && getTransitionDurationFromElement(overlay);
          removeClass(modal, inClass);
          modal[setAttribute](ariaHidden, true);
          setTimeout(function() {
            hasClass(modal, "fade") ? emulateTransitionEnd(modal, triggerHide) : triggerHide();
          }, supportTransitions && overlay && overlayDelay ? overlayDelay : 2);
        }, 2);
      };
      this.setContent = function(content2) {
        queryElement("." + component + "-content", modal)[innerHTML] = content2;
      };
      this.update = function() {
        if (hasClass(modal, inClass)) {
          checkScrollbar();
          setScrollbar();
        }
      };
      if (!!element && !(stringModal in element)) {
        on(element, clickEvent, clickHandler);
      }
      if (!!self2[content]) {
        self2.setContent(self2[content]);
      }
      if (element) {
        element[stringModal] = self2;
        modal[modalTrigger] = element;
      } else {
        modal[stringModal] = self2;
      }
    };
    supports[push]([stringModal, Modal, "[" + dataToggle + '="modal"]']);
    var Popover = function(element, options) {
      element = queryElement(element);
      options = options || {};
      var triggerData = element[getAttribute](dataTrigger), animationData = element[getAttribute](dataAnimation), placementData = element[getAttribute](dataPlacement), dismissibleData = element[getAttribute](dataDismissible), delayData = element[getAttribute](dataDelay), containerData = element[getAttribute](dataContainer), component = "popover", template = "template", trigger = "trigger", classString = "class", div = "div", fade = "fade", dataContent = "data-content", dismissible = "dismissible", closeBtn = '<button type="button" class="close">\xD7</button>', containerElement = queryElement(options[container]), containerDataElement = queryElement(containerData), modal = getClosest(element, ".modal"), navbarFixedTop = getClosest(element, "." + fixedTop), navbarFixedBottom = getClosest(element, "." + fixedBottom);
      this[template] = options[template] ? options[template] : null;
      this[trigger] = options[trigger] ? options[trigger] : triggerData || hoverEvent;
      this[animation] = options[animation] && options[animation] !== fade ? options[animation] : animationData || fade;
      this[placement] = options[placement] ? options[placement] : placementData || top;
      this[delay] = parseInt(options[delay] || delayData) || 200;
      this[dismissible] = options[dismissible] || dismissibleData === "true" ? true : false;
      this[container] = containerElement ? containerElement : containerDataElement ? containerDataElement : navbarFixedTop ? navbarFixedTop : navbarFixedBottom ? navbarFixedBottom : modal ? modal : DOC[body];
      var self2 = this, titleString = options.title || element[getAttribute](dataTitle) || null, contentString = options.content || element[getAttribute](dataContent) || null;
      if (!contentString && !this[template])
        return;
      var popover = null, timer = 0, placementSetting = this[placement], dismissibleHandler = function(e) {
        if (popover !== null && e[target] === queryElement(".close", popover)) {
          self2.hide();
        }
      }, removePopover = function() {
        self2[container].removeChild(popover);
        timer = null;
        popover = null;
      }, createPopover = function() {
        titleString = options.title || element[getAttribute](dataTitle);
        contentString = options.content || element[getAttribute](dataContent);
        contentString = !!contentString ? contentString.replace(/^\s+|\s+$/g, "") : null;
        popover = DOC[createElement](div);
        if (contentString !== null && self2[template] === null) {
          popover[setAttribute]("role", "tooltip");
          if (titleString !== null) {
            var popoverTitle = DOC[createElement]("h3");
            popoverTitle[setAttribute](classString, component + "-title");
            popoverTitle[innerHTML] = self2[dismissible] ? titleString + closeBtn : titleString;
            popover[appendChild](popoverTitle);
          }
          var popoverArrow = DOC[createElement](div), popoverContent = DOC[createElement](div);
          popoverArrow[setAttribute](classString, "arrow");
          popoverContent[setAttribute](classString, component + "-content");
          popover[appendChild](popoverArrow);
          popover[appendChild](popoverContent);
          popoverContent[innerHTML] = self2[dismissible] && titleString === null ? contentString + closeBtn : contentString;
        } else {
          var popoverTemplate = DOC[createElement](div);
          self2[template] = self2[template].replace(/^\s+|\s+$/g, "");
          popoverTemplate[innerHTML] = self2[template];
          popover[innerHTML] = popoverTemplate.firstChild[innerHTML];
        }
        self2[container][appendChild](popover);
        popover[style].display = "block";
        popover[setAttribute](classString, component + " " + placementSetting + " " + self2[animation]);
      }, showPopover = function() {
        !hasClass(popover, inClass) && addClass(popover, inClass);
      }, updatePopover = function() {
        styleTip(element, popover, placementSetting, self2[container]);
      }, dismissHandlerToggle = function(type) {
        if (clickEvent == self2[trigger] || self2[trigger] == "focus") {
          !self2[dismissible] && type(element, "blur", self2.hide);
        }
        self2[dismissible] && type(DOC, clickEvent, dismissibleHandler);
        !isIE8 && type(globalObject, resizeEvent, self2.hide, passiveHandler);
      }, showTrigger = function() {
        dismissHandlerToggle(on);
        bootstrapCustomEvent.call(element, shownEvent, component);
      }, hideTrigger = function() {
        dismissHandlerToggle(off);
        removePopover();
        bootstrapCustomEvent.call(element, hiddenEvent, component);
      };
      this.toggle = function() {
        if (popover === null) {
          self2.show();
        } else {
          self2.hide();
        }
      };
      this.show = function() {
        clearTimeout(timer);
        timer = setTimeout(function() {
          if (popover === null) {
            placementSetting = self2[placement];
            createPopover();
            updatePopover();
            showPopover();
            bootstrapCustomEvent.call(element, showEvent, component);
            !!self2[animation] ? emulateTransitionEnd(popover, showTrigger) : showTrigger();
          }
        }, 20);
      };
      this.hide = function() {
        clearTimeout(timer);
        timer = setTimeout(function() {
          if (popover && popover !== null && hasClass(popover, inClass)) {
            bootstrapCustomEvent.call(element, hideEvent, component);
            removeClass(popover, inClass);
            !!self2[animation] ? emulateTransitionEnd(popover, hideTrigger) : hideTrigger();
          }
        }, self2[delay]);
      };
      if (!(stringPopover in element)) {
        if (self2[trigger] === hoverEvent) {
          on(element, mouseHover[0], self2.show);
          if (!self2[dismissible]) {
            on(element, mouseHover[1], self2.hide);
          }
        } else if (clickEvent == self2[trigger] || self2[trigger] == "focus") {
          on(element, self2[trigger], self2.toggle);
        }
      }
      element[stringPopover] = self2;
    };
    supports[push]([stringPopover, Popover, "[" + dataToggle + '="popover"]']);
    var ScrollSpy = function(element, options) {
      element = queryElement(element);
      var targetData = queryElement(element[getAttribute](dataTarget)), offsetData = element[getAttribute]("data-offset");
      options = options || {};
      if (!options[target] && !targetData) {
        return;
      }
      var self2 = this, spyTarget = options[target] && queryElement(options[target]) || targetData, links = spyTarget && spyTarget[getElementsByTagName]("A"), offset = parseInt(options["offset"] || offsetData) || 10, items = [], targetItems = [], scrollOffset, scrollTarget = element[offsetHeight] < element[scrollHeight] ? element : globalObject, isWindow = scrollTarget === globalObject;
      for (var i = 0, il = links[length]; i < il; i++) {
        var href = links[i][getAttribute]("href"), targetItem = href && href.charAt(0) === "#" && href.slice(-1) !== "#" && queryElement(href);
        if (!!targetItem) {
          items[push](links[i]);
          targetItems[push](targetItem);
        }
      }
      var updateItem = function(index) {
        var parent = items[index][parentNode], targetItem2 = targetItems[index], dropdown = getClosest(parent, ".dropdown"), targetRect = isWindow && targetItem2[getBoundingClientRect](), isActive = hasClass(parent, active) || false, topEdge = (isWindow ? targetRect[top] + scrollOffset : targetItem2[offsetTop]) - offset, bottomEdge = isWindow ? targetRect[bottom] + scrollOffset - offset : targetItems[index + 1] ? targetItems[index + 1][offsetTop] - offset : element[scrollHeight], inside = scrollOffset >= topEdge && bottomEdge > scrollOffset;
        if (!isActive && inside) {
          if (parent.tagName === "LI" && !hasClass(parent, active)) {
            addClass(parent, active);
            if (dropdown && !hasClass(dropdown, active)) {
              addClass(dropdown, active);
            }
            bootstrapCustomEvent.call(element, "activate", "scrollspy", items[index]);
          }
        } else if (!inside) {
          if (parent.tagName === "LI" && hasClass(parent, active)) {
            removeClass(parent, active);
            if (dropdown && hasClass(dropdown, active) && !getElementsByClassName(parent[parentNode], active).length) {
              removeClass(dropdown, active);
            }
          }
        } else if (!inside && !isActive || isActive && inside) {
          return;
        }
      }, updateItems = function() {
        scrollOffset = isWindow ? getScroll().y : element[scrollTop];
        for (var index = 0, itl = items[length]; index < itl; index++) {
          updateItem(index);
        }
      };
      this.refresh = function() {
        updateItems();
      };
      if (!(stringScrollSpy in element)) {
        on(scrollTarget, scrollEvent, self2.refresh, passiveHandler);
        !isIE8 && on(globalObject, resizeEvent, self2.refresh, passiveHandler);
      }
      self2.refresh();
      element[stringScrollSpy] = self2;
    };
    supports[push]([stringScrollSpy, ScrollSpy, "[" + dataSpy + '="scroll"]']);
    var Tab = function(element, options) {
      element = queryElement(element);
      var heightData = element[getAttribute](dataHeight), component = "tab", height2 = "height", float = "float", isAnimating = "isAnimating";
      options = options || {};
      this[height2] = supportTransitions ? options[height2] || heightData === "true" : false;
      var self2 = this, next, tabs = getClosest(element, ".nav"), tabsContentContainer = false, dropdown = tabs && queryElement(".dropdown", tabs), activeTab, activeContent, nextContent, containerHeight, equalContents, nextHeight, triggerEnd = function() {
        tabsContentContainer[style][height2] = "";
        removeClass(tabsContentContainer, collapsing);
        tabs[isAnimating] = false;
      }, triggerShow = function() {
        if (tabsContentContainer) {
          if (equalContents) {
            triggerEnd();
          } else {
            setTimeout(function() {
              tabsContentContainer[style][height2] = nextHeight + "px";
              tabsContentContainer[offsetWidth];
              emulateTransitionEnd(tabsContentContainer, triggerEnd);
            }, 50);
          }
        } else {
          tabs[isAnimating] = false;
        }
        bootstrapCustomEvent.call(next, shownEvent, component, activeTab);
      }, triggerHide = function() {
        if (tabsContentContainer) {
          activeContent[style][float] = left;
          nextContent[style][float] = left;
          containerHeight = activeContent[scrollHeight];
        }
        addClass(nextContent, active);
        bootstrapCustomEvent.call(next, showEvent, component, activeTab);
        removeClass(activeContent, active);
        bootstrapCustomEvent.call(activeTab, hiddenEvent, component, next);
        if (tabsContentContainer) {
          nextHeight = nextContent[scrollHeight];
          equalContents = nextHeight === containerHeight;
          addClass(tabsContentContainer, collapsing);
          tabsContentContainer[style][height2] = containerHeight + "px";
          tabsContentContainer[offsetHeight];
          activeContent[style][float] = "";
          nextContent[style][float] = "";
        }
        if (hasClass(nextContent, "fade")) {
          setTimeout(function() {
            addClass(nextContent, inClass);
            emulateTransitionEnd(nextContent, triggerShow);
          }, 20);
        } else {
          triggerShow();
        }
      };
      if (!tabs)
        return;
      tabs[isAnimating] = false;
      var getActiveTab = function() {
        var activeTabs = getElementsByClassName(tabs, active), activeTab2;
        if (activeTabs[length] === 1 && !hasClass(activeTabs[0], "dropdown")) {
          activeTab2 = activeTabs[0];
        } else if (activeTabs[length] > 1) {
          activeTab2 = activeTabs[activeTabs[length] - 1];
        }
        return activeTab2[getElementsByTagName]("A")[0];
      }, getActiveContent = function() {
        return queryElement(getActiveTab()[getAttribute]("href"));
      }, clickHandler = function(e) {
        e[preventDefault]();
        next = e[currentTarget] || this;
        !tabs[isAnimating] && !hasClass(next[parentNode], active) && self2.show();
      };
      this.show = function() {
        next = next || element;
        nextContent = queryElement(next[getAttribute]("href"));
        activeTab = getActiveTab();
        activeContent = getActiveContent();
        tabs[isAnimating] = true;
        removeClass(activeTab[parentNode], active);
        activeTab[setAttribute](ariaExpanded, "false");
        addClass(next[parentNode], active);
        next[setAttribute](ariaExpanded, "true");
        if (dropdown) {
          if (!hasClass(element[parentNode][parentNode], "dropdown-menu")) {
            if (hasClass(dropdown, active))
              removeClass(dropdown, active);
          } else {
            if (!hasClass(dropdown, active))
              addClass(dropdown, active);
          }
        }
        bootstrapCustomEvent.call(activeTab, hideEvent, component, next);
        if (hasClass(activeContent, "fade")) {
          removeClass(activeContent, inClass);
          emulateTransitionEnd(activeContent, triggerHide);
        } else {
          triggerHide();
        }
      };
      if (!(stringTab in element)) {
        on(element, clickEvent, clickHandler);
      }
      if (self2[height2]) {
        tabsContentContainer = getActiveContent()[parentNode];
      }
      element[stringTab] = self2;
    };
    supports[push]([stringTab, Tab, "[" + dataToggle + '="tab"]']);
    var Tooltip = function(element, options) {
      element = queryElement(element);
      options = options || {};
      var animationData = element[getAttribute](dataAnimation), placementData = element[getAttribute](dataPlacement), delayData = element[getAttribute](dataDelay), containerData = element[getAttribute](dataContainer), component = "tooltip", classString = "class", title = "title", fade = "fade", div = "div", containerElement = queryElement(options[container]), containerDataElement = queryElement(containerData), modal = getClosest(element, ".modal"), navbarFixedTop = getClosest(element, "." + fixedTop), navbarFixedBottom = getClosest(element, "." + fixedBottom);
      this[animation] = options[animation] && options[animation] !== fade ? options[animation] : animationData || fade;
      this[placement] = options[placement] ? options[placement] : placementData || top;
      this[delay] = parseInt(options[delay] || delayData) || 200;
      this[container] = containerElement ? containerElement : containerDataElement ? containerDataElement : navbarFixedTop ? navbarFixedTop : navbarFixedBottom ? navbarFixedBottom : modal ? modal : DOC[body];
      var self2 = this, timer = 0, placementSetting = this[placement], tooltip = null, titleString = element[getAttribute](title) || element[getAttribute](dataTitle) || element[getAttribute](dataOriginalTitle);
      if (!titleString || titleString == "")
        return;
      var removeToolTip = function() {
        self2[container].removeChild(tooltip);
        tooltip = null;
        timer = null;
      }, createToolTip = function() {
        titleString = element[getAttribute](title) || element[getAttribute](dataTitle) || element[getAttribute](dataOriginalTitle);
        if (!titleString || titleString == "")
          return false;
        tooltip = DOC[createElement](div);
        tooltip[setAttribute]("role", component);
        var tooltipArrow = DOC[createElement](div), tooltipInner = DOC[createElement](div);
        tooltipArrow[setAttribute](classString, component + "-arrow");
        tooltipInner[setAttribute](classString, component + "-inner");
        tooltip[appendChild](tooltipArrow);
        tooltip[appendChild](tooltipInner);
        tooltipInner[innerHTML] = titleString;
        self2[container][appendChild](tooltip);
        tooltip[setAttribute](classString, component + " " + placementSetting + " " + self2[animation]);
      }, updateTooltip = function() {
        styleTip(element, tooltip, placementSetting, self2[container]);
      }, showTooltip = function() {
        !hasClass(tooltip, inClass) && addClass(tooltip, inClass);
      }, showTrigger = function() {
        bootstrapCustomEvent.call(element, shownEvent, component);
        !isIE8 && on(globalObject, resizeEvent, self2.hide, passiveHandler);
      }, hideTrigger = function() {
        !isIE8 && off(globalObject, resizeEvent, self2.hide, passiveHandler);
        removeToolTip();
        bootstrapCustomEvent.call(element, hiddenEvent, component);
      };
      this.show = function() {
        clearTimeout(timer);
        timer = setTimeout(function() {
          if (tooltip === null) {
            placementSetting = self2[placement];
            if (createToolTip() == false)
              return;
            updateTooltip();
            showTooltip();
            bootstrapCustomEvent.call(element, showEvent, component);
            !!self2[animation] ? emulateTransitionEnd(tooltip, showTrigger) : showTrigger();
          }
        }, 20);
      };
      this.hide = function() {
        clearTimeout(timer);
        timer = setTimeout(function() {
          if (tooltip && hasClass(tooltip, inClass)) {
            bootstrapCustomEvent.call(element, hideEvent, component);
            removeClass(tooltip, inClass);
            !!self2[animation] ? emulateTransitionEnd(tooltip, hideTrigger) : hideTrigger();
          }
        }, self2[delay]);
      };
      this.toggle = function() {
        if (!tooltip) {
          self2.show();
        } else {
          self2.hide();
        }
      };
      if (!(stringTooltip in element)) {
        element[setAttribute](dataOriginalTitle, titleString);
        element.removeAttribute(title);
        on(element, mouseHover[0], self2.show);
        on(element, mouseHover[1], self2.hide);
      }
      element[stringTooltip] = self2;
    };
    supports[push]([stringTooltip, Tooltip, "[" + dataToggle + '="tooltip"]']);
    var initializeDataAPI = function(constructor, collection) {
      for (var i = 0, l = collection[length]; i < l; i++) {
        new constructor(collection[i]);
      }
    }, initCallback = BSN.initCallback = function(lookUp) {
      lookUp = lookUp || DOC;
      for (var i = 0, l = supports[length]; i < l; i++) {
        initializeDataAPI(supports[i][1], lookUp[querySelectorAll](supports[i][2]));
      }
    };
    DOC[body] ? initCallback() : on(DOC, "DOMContentLoaded", function() {
      initCallback();
    });
    return {
      Affix,
      Alert,
      Button,
      Carousel,
      Collapse,
      Dropdown,
      Modal,
      Popover,
      ScrollSpy,
      Tab,
      Tooltip
    };
  });
});
export default bootstrapNative;
