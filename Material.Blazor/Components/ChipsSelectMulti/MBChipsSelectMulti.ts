﻿import { MDCChipSet } from '@material/chips';

export function init(elem, isSingleSelect, dotNetObject) {
    if (!elem) {
        return;
    }
    elem._chipSet = MDCChipSet.attachTo(elem);
    elem._isSingleSelect = isSingleSelect;

    const clickedCallback = () => {
        if (elem._isSingleSelect) {
            var selectedChips = elem._chipSet.chips.filter(x => x.foundation.isSelected());

            if (selectedChips.length == 0) {
                dotNetObject.invokeMethodAsync('NotifySingleSelected', -1);
            }
            else {
                dotNetObject.invokeMethodAsync('NotifySingleSelected', elem._chipSet.chips.findIndex(x => x.id === selectedChips[0].id));
            }
        }
        else {
            dotNetObject.invokeMethodAsync('NotifyMultiSelected', elem._chipSet.chips.map(x => x.foundation.isSelected()));
        }
    };

    elem._chipSet.listen('MDCChip:selection', clickedCallback);
}

export function setDisabled(elem, value) {
    if (!elem) {
        return;
    }
    elem._chipSet.disabled = value;
}

// This function doesn't appear to work properly - see https://github.com/Material-Blazor/Material.Blazor/issues/366
export function setSelected(elem, selectedFlags) {
    if (!elem) {
        return;
    }
    for (let i = 0; i < selectedFlags.length; i++) {
        //elem._chipSet.chips[i].selected = selectedFlags[i];
        elem._chipSet.foundation.adapter.selectChipAtIndex(i, selectedFlags[i], false);
        //elem._chipSet.chips[i].foundation.setSelectedFromChipSet(selectedFlags[i], false);
        //elem._chipSet.chips[i].foundation.notifySelection(selectedFlags[i], false);
    }
}
