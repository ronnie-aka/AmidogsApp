import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-information',
  templateUrl: './information.page.html',
  styleUrls: ['./information.page.scss'],
})
export class InformationPage {
  activeSection: string | null = null;
  activeSubSection: string | null = null;

  toggleSection(section: string) {
    if (this.activeSection === section) {
      this.activeSection = null;
      this.activeSubSection = null;
    } else {
      this.activeSection = section;
      this.activeSubSection = null; // Reset sub-section when changing section
    }
  }

  toggleSubSection(subSection: string) {
    if (this.activeSubSection === subSection) {
      this.activeSubSection = null;
    } else {
      this.activeSubSection = subSection;
    }
  }
}
