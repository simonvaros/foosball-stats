import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlayerRoutingModule } from './player-routing.module';
import { PlayerDetailComponent } from './player-detail/player-detail.component';
import { PlayersListComponent } from './players-list/players-list.component';
import { DxToolbarModule } from 'devextreme-angular/ui/toolbar';
import { DxDataGridModule, DxSwitchModule } from 'devextreme-angular';

@NgModule({
  declarations: [PlayerDetailComponent, PlayersListComponent],
  imports: [
    CommonModule,
    PlayerRoutingModule,
    DxToolbarModule,
    DxDataGridModule,
    DxSwitchModule
  ],
})
export class PlayerModule {}
